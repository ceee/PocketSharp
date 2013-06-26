using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  /// <summary>
  /// Base for Responses
  /// </summary>
  [DataContract]
  class ResponseBase
  {
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="ResponseBase"/> is status.
    /// </summary>
    /// <value>
    ///   <c>true</c> if status is OK; otherwise, <c>false</c>.
    /// </value>
    [DataMember(Name = "status")]
    public bool Status { get; set; }
  }
}
