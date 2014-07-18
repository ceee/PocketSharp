using PocketSharp.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PocketSharp
{
  /// <summary>
  /// PocketClient
  /// </summary>
  public partial class PocketClient
  {
    /// <summary>
    /// Retrieves the requestCode from Pocket, which is used to generate the Authentication URI to authenticate the user
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="System.NullReferenceException">Authentication methods need a callbackUri on initialization of the PocketClient class</exception>
    /// <exception cref="PocketException"></exception>
    public async Task<string> GetRequestCode(CancellationToken cancellationToken = default(CancellationToken))
    {
      // check if request code is available
      if (CallbackUri == null)
      {
        throw new NullReferenceException("Authentication methods need a callbackUri on initialization of the PocketClient class");
      }

      // do request
      RequestCode response = await Request<RequestCode>("oauth/request", cancellationToken, new Dictionary<string, string>()
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
    /// <param name="requestCode">The requestCode. If no requestCode is supplied, the property from the PocketClient intialization is used.</param>
    /// <returns>A valid URI to redirect the user to.</returns>
    /// <exception cref="System.NullReferenceException">Call GetRequestCode() first to receive a request_code</exception>
    public Uri GenerateAuthenticationUri(string requestCode = null)
    {
      // check if request code is available
      if (RequestCode == null && requestCode == null)
      {
        throw new NullReferenceException("Call GetRequestCode() first to receive a request_code");
      }

      // override property with given param if available
      if (requestCode != null)
      {
        RequestCode = requestCode;
      }

      return new Uri(String.Format(authentificationUri, RequestCode, CallbackUri, isMobileClient ? "1" : "0", "login", useInsideWebAuthenticationBroker ? "1" : "0"));
    }


    /// <summary>
    /// Generate registration URI from requestCode
    /// Follow the steps as with GenerateAuthenticationUri, but for unregistered users
    /// </summary>
    /// <param name="requestCode">The requestCode. If no requestCode is supplied, the property from the PocketClient intialization is used.</param>
    /// <returns>A valid URI to redirect the user to.</returns>
    /// <exception cref="System.NullReferenceException">Call GetRequestCode() first to receive a request_code</exception>
    public Uri GenerateRegistrationUri(string requestCode = null)
    {
      // check if request code is available
      if (RequestCode == null && requestCode == null)
      {
        throw new NullReferenceException("Call GetRequestCode() first to receive a request_code");
      }

      // override property with given param if available
      if (requestCode != null)
      {
        RequestCode = requestCode;
      }

      return new Uri(String.Format(authentificationUri, RequestCode, CallbackUri, isMobileClient ? "1" : "0", "signup", useInsideWebAuthenticationBroker ? "1" : "0"));
    }


    /// <summary>
    /// Requests the access code and username after authentication
    /// The access code has to permanently be stored within the users session, and should be passed in the constructor for all future PocketClient initializations.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="requestCode">The request code.</param>
    /// <returns>
    /// The authenticated user
    /// </returns>
    /// <exception cref="System.NullReferenceException">Call GetRequestCode() first to receive a request_code</exception>
    public async Task<PocketUser> GetUser(string requestCode = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      // check if request code is available
      if (RequestCode == null && requestCode == null)
      {
        throw new NullReferenceException("Call GetRequestCode() first to receive a request_code");
      }

      // override property with given param if available
      if (requestCode != null)
      {
        RequestCode = requestCode;
      }

      // do request
      PocketUser response = await Request<PocketUser>("oauth/authorize", cancellationToken, new Dictionary<string, string>()
      {
        {"code", RequestCode}
      }, false);

      // save code to client
      AccessCode = response.Code;

      return response;
    }
  }
}
