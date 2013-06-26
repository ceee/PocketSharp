using System;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  /// <summary>
  /// Image
  /// </summary>
  [DataContract]
  public class PocketImage
  {
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    /// <value>
    /// The ID.
    /// </value>
    [DataMember(Name = "image_id")]
    public string ID { get; set; }

    /// <summary>
    /// Gets or sets the caption.
    /// </summary>
    /// <value>
    /// The caption.
    /// </value>
    [DataMember]
    public string Caption { get; set; }

    /// <summary>
    /// Gets or sets the credit.
    /// </summary>
    /// <value>
    /// The credit.
    /// </value>
    [DataMember]
    public string Credit { get; set; }

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
