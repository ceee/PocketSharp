using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  /// <summary>
  /// Access Code
  /// </summary>
  [DataContract]
  internal class AccessCode
  {
    /// <summary>
    /// Gets or sets the code.
    /// </summary>
    /// <value>
    /// The code.
    /// </value>
    [DataMember(Name = "access_token")]
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    /// <value>
    /// The username.
    /// </value>
    [DataMember]
    public string Username { get; set; }
  }
}
