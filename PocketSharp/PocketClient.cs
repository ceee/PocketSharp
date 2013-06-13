using PocketSharp.Models.Authentification;
using RestSharp;
using System;
using System.Net;

namespace PocketSharp
{
  public partial class PocketClient : IPocketClient
  {
    /// <summary>
    /// REST client used for the API communication
    /// </summary>
    private readonly RestClient _restClient;

    /// <summary>
    /// default base URL for the API, which is used, when no baseURL is delivered
    /// </summary>
    private static Uri defaultUrl = new Uri("https://getpocket.com/v3/");

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
    protected string AccessCode { get; set; }


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
    public IRestResponse Request(RestRequest request)
    {
      return _restClient.Execute(request);
    }

    /// <summary>
    /// Makes a typed HTTP REST request to the API
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    public T Request<T>(RestRequest request) where T : new()
    {
      IRestResponse<T> response = _restClient.Execute<T>(request);

      LastRequestData = response;

      if(response.StatusCode != HttpStatusCode.OK)
      {
        throw new APIException(response.Content, response.ErrorException);
      }
      else if(response.ErrorException != null)
      {
        throw new APIException("Error retrieving response", response.ErrorException);
      }

      return response.Data;
    }



    public void Test()
    {
      var request = new RestRequest("oauth/request", Method.POST);

      request.AddParameter("redirect_uri", "http://ceecore.com");

      RequestCode rawResponse = Request<RequestCode>(request);

      RequestCode = rawResponse.Code;
    }

    public string Test2()
    {
      return string.Format("https://getpocket.com/auth/authorize?request_token={0}&redirect_uri={1}", RequestCode, Uri.EscapeDataString("http://ceecore.com"));
    }

    public AccessCode Test3()
    {
      var request = new RestRequest("oauth/authorize", Method.POST);

      request.AddParameter("code", "fbe15035-d6b1-e4c2-590b-60de6e");

      AccessCode rawResponse = Request<AccessCode>(request);

      return rawResponse;
    }

    public bool Test4(string code)
    {
      var request = new RestRequest("get", Method.POST);

      request.AddParameter("access_toiken", code);
      request.AddParameter("favorite", "1");

      AccessCode rawResponse = Request<AccessCode>(request);

      return true;
    }
  }
}
