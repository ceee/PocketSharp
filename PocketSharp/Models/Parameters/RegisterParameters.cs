using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Linq;

namespace PocketSharp.Models
{
  /// <summary>
  /// All parameters which can be passed to register a user
  /// </summary>
  [DataContract]
  internal class RegisterParameters : Parameters
  {

    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    /// <value>
    /// The username.
    /// </value>
    [DataMember(Name = "username")]
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the E-Mail.
    /// </summary>
    /// <value>
    /// The E-Mail.
    /// </value>
    [DataMember(Name = "email")]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    /// <value>
    /// The password.
    /// </value>
    [DataMember(Name = "password")]
    public string Password { get; set; }
  }
}
