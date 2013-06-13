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
    /// Returns all associated data from the last request
    /// </summary>
    IRestResponse LastRequestData { get; }

    /// <summary>
    /// Makes a HTTP REST request to the API
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    string Request(RestRequest request);

    /// <summary>
    /// Makes a typed HTTP REST request to the API
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    T Request<T>(RestRequest request) where T : new();
  }
}
