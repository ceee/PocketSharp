using PocketSharp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using ServiceStack.Text;

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
    protected readonly RestClient _restClient;

    /// <summary>
    /// default base URL for the API
    /// </summary>
    protected static Uri defaultBaseUrl = new Uri("https://getpocket.com/v3/");

    /// <summary>
    /// The authentification URL
    /// </summary>
    protected static string authentificationUrl = defaultBaseUrl + "auth/authorize?request_token={0}&redirect_uri={1}";

    /// <summary>
    /// base URL for the API
    /// </summary>
    protected Uri BaseUrl { get; set; }

    /// <summary>
    /// Accessor for the Pocket API key
    /// see: http://getpocket.com/developer
    /// </summary>
    public string ConsumerKey { get; set; }

    /// <summary>
    /// Returns all associated data from the last request
    /// </summary>
    public IRestResponse LastRequestData { get; private set; }

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
    /// <param name="consumerKey">The API key.</param>
    public PocketClient(string consumerKey)
      : this(consumerKey, "", defaultBaseUrl) { }


    /// <summary>
    /// Initializes a new instance of the <see cref="PocketClient" /> class.
    /// </summary>
    /// <param name="consumerKey">The API key.</param>
    /// <param name="accessCode">The access code.</param>
    public PocketClient(string consumerKey, string accessCode)
      : this(consumerKey, accessCode, defaultBaseUrl) { }


    /// <summary>
    /// Initializes a new instance of the <see cref="PocketClient" /> class.
    /// </summary>
    /// <param name="consumerKey">The API key.</param>
    /// <param name="baseUrl">The base URL.</param>
    public PocketClient(string consumerKey, Uri baseUrl)
      : this(consumerKey, "", baseUrl) { }


    /// <summary>
    /// Initializes a new instance of the <see cref="PocketClient"/> class.
    /// </summary>
    /// <param name="consumerKey">The API key.</param>
    /// <param name="accessCode">Provide an access code if the user is already authenticated</param>
    /// <param name="baseUrl">The base URL.</param>
    public PocketClient(string consumerKey, string accessCode, Uri baseUrl)
    {
      // assign public properties
      BaseUrl = baseUrl;
      ConsumerKey = consumerKey;

      // assign access code if submitted
      if (accessCode != "")
      {
        AccessCode = accessCode.ToString();
      }

      // initialize REST client
      _restClient = new RestClient
      {
        BaseUrl = BaseUrl.ToString()
      };

      // add default parameters to each request
      _restClient.AddDefaultParameter("consumer_key", ConsumerKey);

      // Pocket needs this specific Accept header :-S
      _restClient.AddDefaultHeader("Accept", "*/*");

      // defines the response format (according to the Pocket docs)
      _restClient.AddDefaultHeader("X-Accept", "application/json");

      // custom JSON deserializer (ServiceStack.Text)
      _restClient.AddHandler("application/json", new JsonDeserializer());

      // add custom deserialization lambdas
      JsonDeserializer.AddCustomDeserialization();
    }


    /// <summary>
    /// Makes a HTTP REST request to the API
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    protected string Request(RestRequest request)
    {
      IRestResponse response = _restClient.Execute(request);

      LastRequestData = response;
      ValidateResponse(response);

      return response.Content;
    }

    /// <summary>
    /// Makes a typed HTTP REST request to the API
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    protected T Request<T>(RestRequest request) where T : new()
    {
      IRestResponse<T> response = _restClient.Execute<T>(request);

      LastRequestData = response;
      ValidateResponse(response);

      return response.Data;
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
    protected T Get<T>(string method, List<Parameter> parameters = null, bool requireAuth = false) where T : class, new()
    {
      if (requireAuth && AccessCode == null)
      {
        throw new APIException("No access token available. Use authentification first.");
      }

      // every single Pocket API endpoint requires HTTP POST data
      var request = new RestRequest(method, Method.POST);

      // add access token (necessary for all requests except authentification)
      if (AccessCode != null)
      {
        request.AddParameter("access_token", AccessCode);
      }

      // enumeration for params
      if (parameters != null)
      {
        parameters.ForEach(delegate(Parameter param)
        {
          request.AddParameter(param);
        });
      }

      // do the request
      return Request<T>(request);
    }


    /// <summary>
    /// Puts an action
    /// </summary>
    /// <param name="actionParameter">The action parameter.</param>
    /// <returns></returns>
    protected bool PutSendAction(ActionParameter actionParameter)
    {
      ModifyParameters parameters = new ModifyParameters()
      {
        Actions = new List<ActionParameter>() { actionParameter }
      };

      return Get<Modify>("send", parameters.Convert(), true).Status;
    }


    /// <summary>
    /// Validates the response.
    /// </summary>
    /// <param name="response">The response.</param>
    /// <returns></returns>
    /// <exception cref="APIException">
    /// Error retrieving response
    /// </exception>
    protected void ValidateResponse(IRestResponse response)
    {
      if (response.StatusCode != HttpStatusCode.OK)
      {
        // get pocket error headers
        Parameter error = response.Headers[1];
        Parameter errorCode = response.Headers[2];

        string exceptionString = response.Content;

        bool isPocketError = error.Name == "X-Error";

        // update message to include pocket response data
        if (isPocketError)
        {
          exceptionString = exceptionString + "\nPocketResponse: (" + errorCode.Value + ") " + error.Value;
        }

        // create exception
        APIException exception = new APIException(exceptionString, response.ErrorException);

        if (isPocketError)
        {
          // add custom pocket fields
          exception.PocketError = error.Value.ToString();
          exception.PocketErrorCode = Convert.ToInt32(errorCode.Value);

          // add to generic exception data
          exception.Data.Add(error.Name, error.Value);
          exception.Data.Add(errorCode.Name, errorCode.Value);
        }

        throw exception;
      }
      else if (response.ErrorException != null)
      {
        throw new APIException("Error retrieving response", response.ErrorException);
      }
    }
  }
}
