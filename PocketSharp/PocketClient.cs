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
using System.Linq;

namespace PocketSharp
{
  /// <summary>
  /// PocketClient
  /// </summary>
  public partial class PocketClient : IPocketClient, IDisposable
  {
    /// <summary>
    /// REST client used for the API communication
    /// </summary>
    protected readonly HttpClient httpClient;

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
    protected static string baseUri = "https://getpocket.com/v3/";

    /// <summary>
    /// The base URL for Pocket reader requests
    /// </summary>
    protected static string parserUri = null;

    /// <summary>
    /// The authentification URL
    /// </summary>
    protected string authentificationUri = "https://getpocket.com/auth/authorize?request_token={0}&redirect_uri={1}&mobile={2}&force={3}&webauthenticationbroker={4}";

    /// <summary>
    /// Indicates, whether this client is used for mobile or desktop
    /// </summary>
    protected bool isMobileClient = true;

    /// <summary>
    /// Indicates, whether this client is used inside a broker (on Windows 8)
    /// </summary>
    protected bool useInsideWebAuthenticationBroker = true;

    /// <summary>
    /// Indicates, whether the last HTTP response is cached
    /// </summary>
    protected bool cacheHTTPResponseData;

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
    /// Action which is executed after every request
    /// </summary>
    /// <value>
    /// The after request callback.
    /// </value>
    public Action<string> AfterRequest { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="PocketClient" /> class.
    /// </summary>
    /// <param name="consumerKey">The API key</param>
    /// <param name="accessCode">Provide an access code if the user is already authenticated</param>
    /// <param name="callbackUri">The callback URL is called by Pocket after authentication</param>
    /// <param name="handler">The HttpMessage handler.</param>
    /// <param name="timeout">Request timeout (in seconds).</param>
    /// <param name="isMobileClient">Indicates, whether this client is used for mobile or desktop</param>
    /// <param name="useInsideWebAuthenticationBroker">Indicates, whether this client is used inside a broker (on Windows 8), see: http://getpocket.com/developer/docs/getstarted/windows8 </param>
    /// <param name="parserUri">Enables the wrapper for the private Text Parser API</param>
    /// <param name="cacheHTTPResponseData">Caches the last HTTP response in public properties</param>
    public PocketClient(
      string consumerKey,
      string accessCode = null,
      string callbackUri = null,
      HttpMessageHandler handler = null,
      int? timeout = null,
      bool isMobileClient = true,
      bool useInsideWebAuthenticationBroker = false,
      Uri parserUri = null,
      bool cacheHTTPResponseData = true)
    {
      // assign public properties
      ConsumerKey = consumerKey;

      this.isMobileClient = isMobileClient;
      this.useInsideWebAuthenticationBroker = useInsideWebAuthenticationBroker;
      this.cacheHTTPResponseData = cacheHTTPResponseData;
      PocketClient.parserUri = parserUri?.OriginalString;

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
      httpClient = new HttpClient(handler ?? new HttpClientHandler());

      if (timeout.HasValue)
      {
        httpClient.Timeout = TimeSpan.FromSeconds(timeout.Value);
      }

      // Pocket needs this specific Accept header :-S
      httpClient.DefaultRequestHeaders.Add("Accept", "*/*");

      // defines the response format (according to the Pocket docs)
      httpClient.DefaultRequestHeaders.Add("X-Accept", "application/json");
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
      bool requireAuth = true,
      bool isReaderRequest = false) where T : class, new()
    {
      if (requireAuth && AccessCode == null)
      {
        throw new PocketException("SDK error: No access token available. Use authentication first.");
      }

      string httpBase = isReaderRequest ? parserUri : baseUri;

      if (isReaderRequest && String.IsNullOrEmpty(parserUri))
      {
        throw new PocketException("Please pass a valid parserUri in the PocketClient ctor.");
      }

      // every single Pocket API endpoint requires HTTP POST data
      HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, httpBase + method);
      HttpResponseMessage response = null;
      string responseString = null;

      if (parameters == null)
      {
        parameters = new Dictionary<string, string>();
      }

      // add consumer key to each request
      parameters.Add("consumer_key", ConsumerKey);

      // add access token (necessary for all requests except authentification)
      if (AccessCode != null && requireAuth)
      {
        parameters.Add("access_token", AccessCode);
      }

      // content of the request
      request.Content = new FormUrlEncodedContent(parameters);

      // call pre request action
      PreRequest?.Invoke(method);

      // make async request
      try
      {
        response = await httpClient.SendAsync(request, cancellationToken);

        // validate HTTP response
        ValidateResponse(response);

        // cache headers
        if (cacheHTTPResponseData)
        {
          lastHeaders = response.Headers;
        }

        // read response
        responseString = await response.Content.ReadAsStringAsync();
      }
      catch (HttpRequestException exc)
      {
        throw new PocketException(exc.Message, exc);
      }
      catch (PocketException exc)
      {
        throw exc;
      }
      finally
      {
        request.Dispose();

        if (response != null)
        {
          response.Dispose();
        }
      }

      // call after request action
      AfterRequest?.Invoke(responseString);

      // cache response
      if (cacheHTTPResponseData)
      {
        lastResponseData = responseString;
      }

      return DeserializeJson<T>(responseString);
    }


    /// <summary>
    /// Fetches a string from a resource
    /// </summary>
    /// <param name="uri">URI</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    protected async Task<string> RequestAsString(string uri, CancellationToken cancellationToken)
    {
      // every single Pocket API endpoint requires HTTP POST data
      HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
      HttpResponseMessage response = null;
      string responseString = null;

      // call pre request action
      PreRequest?.Invoke(uri);

      // make async request
      try
      {
        response = await httpClient.GetAsync(uri, cancellationToken);

        // validate HTTP response
        ValidateResponse(response);

        // cache headers
        if (cacheHTTPResponseData)
        {
          lastHeaders = response.Headers;
        }

        // read response
        responseString = await response.Content.ReadAsStringAsync();
      }
      catch (HttpRequestException exc)
      {
        throw new PocketException(exc.Message, exc);
      }
      catch (PocketException exc)
      {
        throw exc;
      }
      finally
      {
        request.Dispose();

        if (response != null)
        {
          response.Dispose();
        }
      }

      // call after request action
      AfterRequest?.Invoke(responseString);

      // cache response
      if (cacheHTTPResponseData)
      {
        lastResponseData = responseString;
      }

      return responseString;
    }


    /// <summary>
    /// Converts JSON to Pocket objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json">Raw JSON response</param>
    /// <returns></returns>
    /// <exception cref="PocketException">Parse error.</exception>
    protected T DeserializeJson<T>(string json) where T : class, new()
    {
      json = json.Replace("[]", "{}");

      // deserialize object
      List<PocketException> errors = new List<PocketException>();

      T parsedResponse = JsonConvert.DeserializeObject<T>(
        json,
        new JsonSerializerSettings
        {
          Error = (object sender, ErrorEventArgs args) =>
          {
            errors.Add(new PocketException(String.Format("Parse error: {0}", args.ErrorContext.Error.Message), args.ErrorContext.Error));
            args.ErrorContext.Handled = true;
          },
          Converters =
          {
            new PocketItemConverter(),
            new BoolConverter(),
            new UnixDateTimeConverter(),
            new NullableIntConverter(),
            new UriConverter()
          }
        }
      );

      if (errors.Count == 1)
      {
        PocketException firstException = errors[0];
        firstException.Data["json"] = json;
        throw firstException;
      }
      else if (errors.Count > 1)
      {
        AggregateException ex = new AggregateException("Unable to parse json. See innner exceptions and exception.Data[\"json\"] for details", errors);
        ex.Data["json"] = json;
        throw ex;
      }

      return parsedResponse;
    }


    /// <summary>
    /// Sends a list of actions
    /// </summary>
    /// <param name="actionParameters">The action parameters.</param>
    /// <returns></returns>
    internal async Task<bool> Send(IEnumerable<PocketAction> actionParameters, CancellationToken cancellationToken)
    {
      foreach (PocketAction action in actionParameters)
      {
        action.Time = action.Time.HasValue ? ((DateTime)action.Time).ToUniversalTime() : action.Time;
      }

      Dictionary<string, string> parameters = new Dictionary<string, string>() {{
        "actions", JsonConvert.SerializeObject(actionParameters.Select(action => action.Convert()))                                                                    
      }};

      return (await Request<Modify>("send", cancellationToken, parameters)).Status;
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


    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      httpClient.Dispose();
      lastHeaders = null;
    }
  }
}
