using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  /// <summary>
  /// Request Code
  /// </summary>
  [DataContract]
  internal class RequestCode
  {
    /// <summary>
    /// Gets or sets the code.
    /// </summary>
    /// <value>
    /// The code.
    /// </value>
    [DataMember]
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    /// <value>
    /// The state.
    /// </value>
    [DataMember]
    public string State { get; set; }
  }
}
