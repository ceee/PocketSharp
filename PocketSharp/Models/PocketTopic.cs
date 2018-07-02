using Newtonsoft.Json;
using System;

namespace PocketSharp.Models
{
  /// <summary>
  /// Topic
  /// </summary>
  [JsonObject]
  public class PocketTopic
  {
    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    /// <value>
    /// The topic category.
    /// </value>
    [JsonProperty("category")]
    public string Category { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name of the topic.
    /// </value>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the URI.
    /// </summary>
    /// <value>
    /// Link to the topic listing on Pocket.
    /// </value>
    [JsonProperty("url")]
    public Uri Uri { get; set; }
  }
}
