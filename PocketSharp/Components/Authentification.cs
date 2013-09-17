using PocketSharp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    public async Task<string> GetRequestCode()
    {
      // check if request code is available
      if (CallbackUri == null)
      {
        throw new PocketException("Authentication methods need a callbackUri on initialization of the PocketClient class");
      }

      // do request
      RequestCode response = await Request<RequestCode>("oauth/request", new Dictionary<string, string>()
      { 
        { "redirect_uri", CallbackUri } 
      }, false);

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
        throw new PocketException("Call GetRequestCode() first to receive a request_code");
      }

      // override property with given param if available
      if(requestCode != null)
      {
        RequestCode = requestCode;
      }

      return new Uri(string.Format(authentificationUri, RequestCode, CallbackUri));
    }


    /// <summary>
    /// Requests the access code after authentification
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetAccessCode(string requestCode = null)
    {
      // check if request code is available
      if(RequestCode == null && requestCode == null)
      {
        throw new PocketException("Call GetRequestCode() first to receive a request_code");
      }

      // override property with given param if available
      if(requestCode != null)
      {
        RequestCode = requestCode;
      }

      // do request
      AccessCode response = await Request<AccessCode>("oauth/authorize", new Dictionary<string, string>()
      { 
        { "code", RequestCode } 
      }, false);

      // save code to client
      AccessCode = response.Code;

      return AccessCode;
    }
  }
}
