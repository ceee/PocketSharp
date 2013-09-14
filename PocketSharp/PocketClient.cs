using PocketSharp.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

      // custom JSON deserializer (ServiceStack.Text)
      //_restClient.AddHandler("application/json", new JsonDeserializer());

      // add custom deserialization lambdas
      //JsonDeserializer.AddCustomDeserialization();
    }


    /// <summary>
    /// Fetches a typed resource
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="method">Requested method (path after /v3/)</param>
    /// <param name="parameters">Additional POST parameters</param>
    /// <param name="requireAuth">if set to <c>true</c> [require auth].</param>
    /// <returns></returns>
    /// <exception cref="APIException">No access token available. Use authentification first.</exception>
    protected async Task<T> Request<T>(string method, List<Parameter> parameters = null, bool requireAuth = false) where T : class, new()
    {
      if (requireAuth && AccessCode == null)
      {
        throw new APIException("No access token available. Use authentification first.");
      }

      // convert parameters
      List<KeyValuePair<string, string>> kvParameters = new List<KeyValuePair<string, string>>();

      if (parameters != null)
      {
        foreach (Parameter item in parameters)
        {
          if (item.Value != null)
          {
            kvParameters.Add(new KeyValuePair<string, string>(item.Name, item.Value.ToString()));
          }
        }
      }

      // every single Pocket API endpoint requires HTTP POST data
      HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, method);

      // add consumer key to each request
      kvParameters.Add(new KeyValuePair<string, string>("consumer_key", ConsumerKey));

      // add access token (necessary for all requests except authentification)
      if (AccessCode != null)
      {
        kvParameters.Add(new KeyValuePair<string, string>("access_token", AccessCode));
      }

      // content of the request
      request.Content = new FormUrlEncodedContent(kvParameters);

      // make async request
      HttpResponseMessage response = await _restClient.SendAsync(request);

      // throw exception if no valid response
      response.EnsureSuccessStatusCode();

      // read response
      string responseString = response.Content.ReadAsStringAsync().Result;

      // deserialize object
      return JsonConvert.DeserializeObject<T>(
        responseString, 
        new BoolConverter(),
        new UnixDateTimeConverter()
      );

      //ValidateResponse(response);
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
    /// <exception cref="APIException">
    /// Error retrieving response
    /// </exception>
    //protected void ValidateResponse(IRestResponse response)
    //{
    //  if (response.StatusCode != HttpStatusCode.OK)
    //  {
    //    // get pocket error headers
    //    Parameter error = response.Headers[1];
    //    Parameter errorCode = response.Headers[2];

    //    string exceptionString = response.Content;

    //    bool isPocketError = error.Name == "X-Error";

    //    // update message to include pocket response data
    //    if (isPocketError)
    //    {
    //      exceptionString = exceptionString + "\nPocketResponse: (" + errorCode.Value + ") " + error.Value;
    //    }

    //    // create exception
    //    APIException exception = new APIException(exceptionString, response.ErrorException);

    //    if (isPocketError)
    //    {
    //      // add custom pocket fields
    //      exception.PocketError = error.Value.ToString();
    //      exception.PocketErrorCode = Convert.ToInt32(errorCode.Value);

    //      // add to generic exception data
    //      exception.Data.Add(error.Name, error.Value);
    //      exception.Data.Add(errorCode.Name, errorCode.Value);
    //    }

    //    throw exception;
    //  }
    //  else if (response.ErrorException != null)
    //  {
    //    throw new APIException("Error retrieving response", response.ErrorException);
    //  }
    //}
  }
}
