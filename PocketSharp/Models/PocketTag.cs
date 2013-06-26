using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  /// <summary>
  /// Tag
  /// </summary>
  [DataContract]
  public class PocketTag
  {
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [DataMember(Name = "tag")]
    public string Name { get; set; }
  }
}
