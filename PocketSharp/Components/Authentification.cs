using PocketSharp.Models.Authentification;
using System;
using System.Collections.Generic;
using RestSharp;

namespace PocketSharp
{
  /// <summary>
  /// PocketClient
  /// </summary>
  public partial class PocketClient
  {
    /// <summary>
    /// Retrieves the requestCode from Pocket.
    /// </summary>
    /// <returns></returns>
    public string GetRequestCode()
    {
      // check if request code is available
      if (CallbackUri == null)
      {
        throw new APIException("Authentication methods need a callbackUri on initialization of the PocketClient class");
      }

      // do request
      RequestCode response = Get<RequestCode>("oauth/request", Utilities.CreateParamInList("redirect_uri", CallbackUri));

      // save code to client
      RequestCode = response.Code;

      // generate redirection URI and return
      return RequestCode;
    }


    /// <summary>
    /// Generate Authentication URI from requestCode
    /// </summary>
    /// <param name="requestCode">The requestCode.</param>
    /// <returns></returns>
    public Uri GenerateAuthenticationUri(string requestCode = null)
    {
      // check if request code is available
      if(RequestCode == null && requestCode == null)
      {
        throw new APIException("Call GetRequestCode() first to receive a request_code");
      }

      // override property with given param if available
      if(requestCode != null)
      {
        RequestCode = requestCode;
      }

      return new Uri(string.Format(authentificationUri, RequestCode, Uri.EscapeDataString(CallbackUri.ToString())));
    }


    /// <summary>
    /// Requests the access code after authentification
    /// </summary>
    /// <returns></returns>
    public string GetAccessCode(string requestCode = null)
    {
      // check if request code is available
      if(RequestCode == null && requestCode == null)
      {
        throw new APIException("Call GetRequestCode() first to receive a request_code");
      }

      // override property with given param if available
      if(requestCode != null)
      {
        RequestCode = requestCode;
      }

      // do request
      AccessCode response = Get<AccessCode>("oauth/authorize", Utilities.CreateParamInList("code", RequestCode));

      // save code to client
      AccessCode = response.Code;

      return AccessCode;
    }
  }
}
