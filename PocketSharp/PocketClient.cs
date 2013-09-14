using PocketSharp.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace PocketSharp
{
  /// <summary>
  /// PocketClient
  /// </summary>
  public partial class PocketClient
  {
    /// <summary>
    /// REST client used for the API communication
    /// </summary>
    protected readonly HttpClient _restClient;

    /// <summary>
    /// The base URL for the Pocket API
    /// </summary>
    protected static Uri baseUri = new Uri("https://getpocket.com/v3/");

    /// <summary>
    /// The authentification URL
    /// </summary>
    protected string authentificationUri = "https://getpocket.com/auth/authorize?request_token={0}&redirect_uri={1}";

    /// <summary>
    /// callback URL for API calls
    /// </summary>
    protected string CallbackUri { get; set; }

    /// <summary>
    /// Accessor for the Pocket API key
    /// see: http://getpocket.com/developer
    /// </summary>
    public string ConsumerKey { get; set; }

    /// <summary>
    /// Code retrieved on authentification
    /// </summary>
    public string RequestCode { get; set; }

    /// <summary>
    /// Code retrieved on authentification-success
    /// </summary>
    public string AccessCode { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="PocketClient"/> class.
    /// </summary>
    /// <param name="consumerKey">The API key</param>
    /// <param name="accessCode">Provide an access code if the user is already authenticated</param>
    /// <param name="callbackUri">The callback URL is called by Pocket after authentication</param>
    public PocketClient(string consumerKey, string accessCode = null, string callbackUri = null)
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
      _restClient = new HttpClient(new HttpClientHandler()
      {
        AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
      });

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
    /// <param name="parameters">Additional POST parameters</param>
    /// <param name="requireAuth">if set to <c>true</c> [require auth].</param>
    /// <returns></returns>
    /// <exception cref="PocketException">No access token available. Use authentification first.</exception>
    protected async Task<T> Request<T>(string method, List<Parameter> parameters = null, bool requireAuth = false) where T : class, new()
    {
      if (requireAuth && AccessCode == null)
      {
        throw new PocketException("SDK error: No access token available. Use authentification first.");
      }

      // convert parameters
      Dictionary<string, string> parameterDict = new Dictionary<string, string>();

      if (parameters != null)
      {
        foreach (Parameter item in parameters)
        {
          if (item.Value != null)
          {
            parameterDict.Add(item.Name, item.Value.ToString());
          }
        }
      }

      // every single Pocket API endpoint requires HTTP POST data
      HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, method);

      // add consumer key to each request
      parameterDict.Add("consumer_key", ConsumerKey);

      // add access token (necessary for all requests except authentification)
      if (AccessCode != null)
      {
        parameterDict.Add("access_token", AccessCode);
      }

      // content of the request
      request.Content = new FormUrlEncodedContent(parameterDict);

      // make async request
      HttpResponseMessage response = await _restClient.SendAsync(request);

      // validate HTTP response
      ValidateResponse(response);

      // read response
      var responseString = await response.Content.ReadAsStringAsync();

      // deserialize object
      T parsedResponse = JsonConvert.DeserializeObject<T>(
        responseString, 
        new JsonSerializerSettings
        {
          Error = (object sender, ErrorEventArgs args) =>
          {
            throw new PocketException(String.Format("Parse error: {0}", args.ErrorContext.Error.Message));
            args.ErrorContext.Handled = true;
          },
          Converters =
          {
            new BoolConverter(),
            new UnixDateTimeConverter()
          }
        }
      );

      return parsedResponse;
    }


    /// <summary>
    /// Puts an action
    /// </summary>
    /// <param name="actionParameter">The action parameter.</param>
    /// <returns></returns>
    protected async Task<bool> PutSendAction(ActionParameter actionParameter)
    {
      ModifyParameters parameters = new ModifyParameters()
      {
        Actions = new List<ActionParameter>() { actionParameter }
      };

      Modify response = await Request<Modify>("send", parameters.Convert(), true);

      return response.Status;
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

      foreach (var header in headers)
      {
        if (header.Key == key)
        {
          var headerEnumerator = header.Value.GetEnumerator();
          headerEnumerator.MoveNext();

          result = headerEnumerator.Current;
        }
      }

      return result;
    }
  }
}
