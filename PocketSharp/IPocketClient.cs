using RestSharp;
using System;

namespace PocketSharp
{
  public interface IPocketClient
  {
    /// <summary>
    /// base URL for the API
    /// </summary>
    Uri BaseUrl { get; set; }

    /// <summary>
    /// Accessor for the Pocket API key
    /// see: http://getpocket.com/developer
    /// </summary>
    string ConsumerKey { get; set; }

    /// <summary>
    /// Code retrieved on authentification
    /// </summary>
    string AuthCode { get; set; }

    /// <summary>
    /// Code retrieved on authentification-success
    /// </summary>
    string AccessCode { get; set; }

    /// <summary>
    /// Makes a HTTP REST request to the API
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    IRestResponse Request(RestRequest request);

    /// <summary>
    /// Makes a typed HTTP REST request to the API
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    T Request<T>(RestRequest request) where T : new();
  }
}
