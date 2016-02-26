using Newtonsoft.Json;
using System;

namespace PocketSharp.Models
{
  /// <summary>
  /// Author
  /// </summary>
  [JsonObject]
  public class PocketAuthor
  {
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    /// <value>
    /// The ID.
    /// </value>
    [JsonProperty("author_id")]
    public string ID { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [JsonProperty]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the URI.
    /// </summary>
    /// <value>
    /// The URI.
    /// </value>
    [JsonProperty("url")]
    public Uri Uri { get; set; }
  }
}
