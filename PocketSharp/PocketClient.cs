using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PocketSharp.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace PocketSharp
{
  /// <summary>
  /// PocketClient
  /// </summary>
  public partial class PocketClient : IPocketClient
  {
    /// <summary>
    /// REST client used for the API communication
    /// </summary>
    protected readonly HttpClient _restClient;

    /// <summary>
    /// Caches HTTP headers from last response
    /// </summary>
    public HttpResponseHeaders lastHeaders;

    /// <summary>
    /// Caches JSON data from last response
    /// </summary>
    public string lastResponseData;

    /// <summary>
    /// The base URL for the Pocket API
    /// </summary>
    protected static Uri baseUri = new Uri("https://getpocket.com/v3/");

    /// <summary>
    /// The authentification URL
    /// </summary>
    protected string authentificationUri = "https://getpocket.com/auth/authorize?request_token={0}&redirect_uri={1}&mobile=1";

    /// <summary>
    /// callback URLi for API calls
    /// </summary>
    /// <value>
    /// The callback URI.
    /// </value>
    public string CallbackUri { get; set; }

    /// <summary>
    /// Accessor for the Pocket API key
    /// see: http://getpocket.com/developer
    /// </summary>
    /// <value>
    /// The consumer key.
    /// </value>
    public string ConsumerKey { get; set; }

    /// <summary>
    /// Code retrieved on authentification
    /// </summary>
    /// <value>
    /// The request code.
    /// </value>
    public string RequestCode { get; set; }

    /// <summary>
    /// Code retrieved on authentification-success
    /// </summary>
    /// <value>
    /// The access code.
    /// </value>
    public string AccessCode { get; set; }

    /// <summary>
    /// Action which is executed before every request
    /// </summary>
    /// <value>
    /// The pre request callback.
    /// </value>
    public Action<string> PreRequest { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="PocketClient" /> class.
    /// </summary>
    /// <param name="consumerKey">The API key</param>
    /// <param name="accessCode">Provide an access code if the user is already authenticated</param>
    /// <param name="callbackUri">The callback URL is called by Pocket after authentication</param>
    /// <param name="handler">The HttpMessage handler.</param>
    /// <param name="timeout">Request timeout (in seconds).</param>
    public PocketClient(string consumerKey, string accessCode = null, string callbackUri = null, HttpMessageHandler handler = null, int? timeout = null)
    {
      // assign public properties
      ConsumerKey = consumerKey;

      // assign access code if submitted
      if (accessCode != null)
      {
        AccessCode = accessCode.ToString();
      }

      // assign callback uri if submitted
      if (callbackUri != null)
      {
        CallbackUri = Uri.EscapeUriString(callbackUri.ToString());
      }

      // initialize REST client
      _restClient = new HttpClient(handler ?? new HttpClientHandler()
      {
        AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
      });

      if (timeout.HasValue)
      {
        _restClient.Timeout = TimeSpan.FromSeconds(timeout.Value);
      }

      // set base uri
      _restClient.BaseAddress = baseUri;

      // Pocket needs this specific Accept header :-S
      _restClient.DefaultRequestHeaders.Add("Accept", "*/*");

      // defines the response format (according to the Pocket docs)
      _restClient.DefaultRequestHeaders.Add("X-Accept", "application/json");
    }


    /// <summary>
    /// Fetches a typed resource
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="method">Requested method (path after /v3/)</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="parameters">Additional POST parameters</param>
    /// <param name="requireAuth">if set to <c>true</c> [require auth].</param>
    /// <returns></returns>
    /// <exception cref="PocketException">No access token available. Use authentification first.</exception>
    protected async Task<T> Request<T>(
      string method,
      CancellationToken cancellationToken,
      Dictionary<string, string> parameters = null,
      bool requireAuth = true) where T : class, new()
    {
      if (requireAuth && AccessCode == null)
      {
        throw new PocketException("SDK error: No access token available. Use authentication first.");
      }

      // every single Pocket API endpoint requires HTTP POST data
      HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, method);
      HttpResponseMessage response = null;

      if (parameters == null)
      {
        parameters = new Dictionary<string, string>();
      }

      // add consumer key to each request
      parameters.Add("consumer_key", ConsumerKey);

      // add access token (necessary for all requests except authentification)
      if (AccessCode != null)
      {
        parameters.Add("access_token", AccessCode);
      }

      // content of the request
      request.Content = new FormUrlEncodedContent(parameters);

      // call pre request action
      if (PreRequest != null)
      {
        PreRequest(method);
      }

      // make async request
      try
      {
        response = await _restClient.SendAsync(request, cancellationToken);
      }
      catch (HttpRequestException exc)
      {
        throw new PocketException(exc.Message, exc);
      }

      // validate HTTP response
      ValidateResponse(response);

      // cache headers
      lastHeaders = response.Headers;

      // read response
      var responseString = await response.Content.ReadAsStringAsync();

      // cache response
      lastResponseData = responseString;

      responseString = responseString.Replace("[]", "{}");

      // deserialize object
      T parsedResponse = JsonConvert.DeserializeObject<T>(
        responseString,
        new JsonSerializerSettings
        {
          Error = (object sender, ErrorEventArgs args) =>
          {
            throw new PocketException(String.Format("Parse error: {0}", args.ErrorContext.Error.Message));
          },
          Converters =
          {
            new BoolConverter(),
            new UnixDateTimeConverter(),
            new NullableIntConverter(),
            new UriConverter()
          }
        }
      );

      return parsedResponse;
    }


    /// <summary>
    /// Sends a list of actions
    /// </summary>
    /// <param name="actionParameters">The action parameters.</param>
    /// <returns></returns>
    internal async Task<bool> Send(List<PocketAction> actionParameters, CancellationToken cancellationToken)
    {
      List<Dictionary<string, object>> actionParamList = new List<Dictionary<string, object>>();

      foreach (var action in actionParameters)
      {
        actionParamList.Add(action.Convert());
      }

      Dictionary<string, string> parameters = new Dictionary<string, string>() {{
        "actions", JsonConvert.SerializeObject(actionParamList)                                                                    
      }};

      Modify response = await Request<Modify>("send", cancellationToken, parameters);

      return response.Status;
    }


    /// <summary>
    /// Sends an action
    /// </summary>
    /// <param name="actionParameter">The action parameter.</param>
    /// <returns></returns>
    internal async Task<bool> Send(PocketAction actionParameter, CancellationToken cancellationToken)
    {
      return await Send(new List<PocketAction>() { actionParameter }, cancellationToken);
    }


    /// <summary>
    /// Validates the response.
    /// </summary>
    /// <param name="response">The response.</param>
    /// <returns></returns>
    /// <exception cref="PocketException">
    /// Error retrieving response
    /// </exception>
    protected void ValidateResponse(HttpResponseMessage response)
    {
      // no error found
      if (response.StatusCode == HttpStatusCode.OK)
      {
        return;
      }

      string exceptionString = response.ReasonPhrase;
      bool isPocketError = response.Headers.Contains("X-Error");

      // fetch custom pocket headers
      string error = TryGetHeaderValue(response.Headers, "X-Error");
      int errorCode = Convert.ToInt32(TryGetHeaderValue(response.Headers, "X-Error-Code"));

      // create exception strings
      if (isPocketError)
      {
        exceptionString = String.Format("Pocket error: {0} ({1}) ", error, errorCode);
      }
      else
      {
        exceptionString = String.Format("Request error: {0} ({1})", response.ReasonPhrase, (int)response.StatusCode);
      }

      // create exception
      PocketException exception = new PocketException(exceptionString);

      if (isPocketError)
      {
        // add custom pocket fields
        exception.PocketError = error;
        exception.PocketErrorCode = errorCode;

        // add to generic exception data
        exception.Data.Add("X-Error", error);
        exception.Data.Add("X-Error-Code", errorCode);
      }

      throw exception;
    }


    /// <summary>
    /// Tries to fetch a header value.
    /// </summary>
    /// <param name="headers">The headers.</param>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    protected string TryGetHeaderValue(HttpResponseHeaders headers, string key)
    {
      string result = null;

      if (headers == null || String.IsNullOrEmpty(key))
      {
        return null;
      }

      foreach (var header in headers)
      {
        if (header.Key == key)
        {
          var headerEnumerator = header.Value.GetEnumerator();
          headerEnumerator.MoveNext();

          result = headerEnumerator.Current;
          break;
        }
      }

      return result;
    }
  }
}
