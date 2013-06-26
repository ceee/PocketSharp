using System;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  /// <summary>
  /// Video
  /// </summary>
  [DataContract]
  public class PocketVideo
  {
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    /// <value>
    /// The ID.
    /// </value>
    [DataMember(Name = "video_id")]
    public string ID { get; set; }

    /// <summary>
    /// Gets or sets the external ID.
    /// </summary>
    /// <value>
    /// The external ID.
    /// </value>
    [DataMember(Name = "vid")]
    public string ExternalID { get; set; }

    /// <summary>
    /// Gets or sets the URI.
    /// </summary>
    /// <value>
    /// The URI.
    /// </value>
    [DataMember(Name = "src")]
    public Uri Uri { get; set; }
  }
}
