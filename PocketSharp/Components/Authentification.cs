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
    public Uri Authenticate(Uri callbackUri)
    {
      RequestCode response = Get<RequestCode>("oauth/request", new List<Parameter>()
      {
        new Parameter() { Name = "redirect_uri", Value = callbackUri, Type = ParameterType.GetOrPost }
      });

      // save code to client
      RequestCode = response.Code;

      // generate redirection URI and return
      return new Uri(string.Format(authentificationUrl, RequestCode, Uri.EscapeDataString(callbackUri.ToString())));
    }


    /// <summary>
    /// Requests the access code after authentification
    /// </summary>
    /// <returns></returns>
    public string GetAccessCode()
    {
      // check if request code is available
      if(RequestCode == null)
      {
        throw new APIException("Authenticate the user first to receive a request_code");
      }

      AccessCode response = Get<AccessCode>("oauth/authorize", new List<Parameter>()
      {
        new Parameter() { Name = "code", Value = RequestCode, Type = ParameterType.GetOrPost }
      });

      // save code to client
      AccessCode = response.Code;

      return AccessCode;
    }
  }
}
