using Newtonsoft.Json;
using System;

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
    /// Gets or sets the Item ID.
    /// </summary>
    /// <value>
    /// The Item ID.
    /// </value>
    [JsonProperty("item_id")]
    public string ItemID { get; set; }

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

    /// <summary>
    /// Gets or sets the URI.
    /// </summary>
    /// <value>
    /// The URI.
    /// </value>
    [JsonProperty("type")]
    [JsonConverter(typeof(VideoTypeConverter))]
    public PocketVideoType Type { get; set; }
  }
}
