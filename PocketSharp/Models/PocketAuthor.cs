using System;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  /// <summary>
  /// Author
  /// </summary>
  [DataContract]
  public class PocketAuthor
  {
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    /// <value>
    /// The ID.
    /// </value>
    [DataMember(Name = "author_id")]
    public string ID { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [DataMember]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the URI.
    /// </summary>
    /// <value>
    /// The URI.
    /// </value>
    [DataMember(Name = "url")]
    public Uri Uri { get; set; }
  }
}
