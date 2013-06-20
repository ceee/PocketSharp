using PocketSharp.Models.Authentification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace PocketSharp
{
  public partial class PocketClient
  {
    /// <summary>
    /// Gets the authentification URI.
    /// </summary>
    /// <param name="callbackUri">The callback URI.</param>
    /// <returns></returns>
    public Uri GetAuthentificationUri(Uri callbackUri)
    {
      RequestCode response = GetResource<RequestCode>("oauth/request", new List<Parameter>()
      {
        new Parameter() { Name = "redirect_uri", Value = callbackUri, Type = ParameterType.GetOrPost }
      });

      // save code to client
      RequestCode = response.Code;

      return new Uri(string.Format(authentificationUrl, RequestCode, Uri.EscapeDataString(callbackUri.ToString())));
    }


    /// <summary>
    /// Authenticates the user
    /// </summary>
    /// <returns></returns>
    public string Authenticate()
    {
      AccessCode response = GetResource<AccessCode>("oauth/authorize", new List<Parameter>()
      {
        new Parameter() { Name = "code", Value = RequestCode, Type = ParameterType.GetOrPost }
      });

      // save code to client
      AccessCode = response.Code;

      return AccessCode;
    }
  }
}
