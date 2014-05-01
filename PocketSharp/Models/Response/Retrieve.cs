using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PocketSharp.Models
{
  /// <summary>
  /// Item Response
  /// </summary>
  [JsonObject]
  internal class Retrieve : ResponseBase
  {
    /// <summary>
    /// Gets or sets the complete.
    /// </summary>
    /// <value>
    /// The complete.
    /// </value>
    [JsonProperty("complete")]
    public int Complete { get; set; }

    /// <summary>
    /// Gets or sets the since.
    /// </summary>
    /// <value>
    /// The since.
    /// </value>
    [JsonProperty("since")]
    public DateTime Since { get; set; }

    /// <summary>
    /// Gets the items.
    /// </summary>
    /// <value>
    /// The items.
    /// </value>
    [JsonProperty("list")]
    [JsonConverter(typeof(ObjectToArrayConverter<PocketItem>))]
    public IEnumerable<PocketItem> Items { get; set; }
  }
}
