using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace PocketSharp
{
  public partial class PocketClient
  {
    /// <summary>
    /// REST client used for the API communication
    /// </summary>
    protected readonly RestClient _restClient;

    /// <summary>
    /// default base URL for the API, which is used, when no baseURL is delivered
    /// </summary>
    protected static Uri defaultUrl = new Uri("https://getpocket.com/v3/");

    /// <summary>
    /// base URL for the API
    /// </summary>
    public Uri BaseUrl { get; set; }

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
    protected string RequestCode { get; set; }

    /// <summary>
    /// Code retrieved on authentification-success
    /// </summary>
    public string AccessCode { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="PocketClient"/> class.
    /// </summary>
    /// <param name="consumerKey">The API key.</param>
    public PocketClient(string consumerKey)
      : this(defaultUrl, consumerKey) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PocketClient"/> class.
    /// </summary>
    /// <param name="baseUrl">The base URL.</param>
    /// <param name="consumerKey">The API key.</param>
    public PocketClient(Uri baseUrl, string consumerKey)
    {
      // assign public properties
      BaseUrl = baseUrl;
      ConsumerKey = consumerKey;

      // initialize REST client
      _restClient = new RestClient
      {
          BaseUrl = baseUrl.ToString()
      };

      // add default parameters to each request
      _restClient.AddDefaultParameter("consumer_key", ConsumerKey);

      // Pocket needs this specific Accept header :-S
      _restClient.AddDefaultHeader("Accept", "*/*");
      
      // defines the response format (according to the Pocket docs)
      _restClient.AddDefaultHeader("X-Accept", "application/json");

      // custom JSON deserializer (ServiceStack.Text)
      _restClient.AddHandler("application/json", new JsonDeserializer());
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
    /// <returns></returns>
    /// <exception cref="APIException">No access token available. Use authentification first.</exception>
    protected T GetResource<T>(string method, List<Parameter> parameters) where T : class, new()
    {
      // every single Pocket API endpoint requires HTTP POST data
      var request = new RestRequest(method, Method.POST);

      // check if access token is available
      if(AccessCode == null)
      {
        throw new APIException("No access token available. Use authentification first.");
      }

      // add access token (necessary for all requests except authentification)
      request.AddParameter("access_token", AccessCode);

      // enumeration for params
      parameters.ForEach(delegate(Parameter param)
      {
        request.AddParameter(param);
      });

      // do the request
      return Request<T>(request);
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
        throw new APIException(response.Content, response.ErrorException);
      }
      else if (response.ErrorException != null)
      {
        throw new APIException("Error retrieving response", response.ErrorException);
      }
    }
  }
}
