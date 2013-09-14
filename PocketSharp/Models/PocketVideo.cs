using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  /// <summary>
  /// Video
  /// </summary>
  [JsonObject]
  public class PocketVideo
  {
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    /// <value>
    /// The ID.
    /// </value>
    [JsonProperty("video_id")]
    public string ID { get; set; }

    /// <summary>
    /// Gets or sets the external ID.
    /// </summary>
    /// <value>
    /// The external ID.
    /// </value>
    [JsonProperty("vid")]
    public string ExternalID { get; set; }

    /// <summary>
    /// Gets or sets the URI.
    /// </summary>
    /// <value>
    /// The URI.
    /// </value>
    [JsonProperty("src")]
    public Uri Uri { get; set; }
  }
}
