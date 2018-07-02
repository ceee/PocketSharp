using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PocketSharp.Models
{
  /// <summary>
  /// Topics Response
  /// </summary>
  [JsonObject]
  internal class TopicsResponse : ResponseBase
  {
    /// <summary>
    /// Gets the topics.
    /// </summary>
    /// <value>
    /// The topics.
    /// </value>
    [JsonProperty("topics")]
    public IEnumerable<PocketTopic> Items { get; set; }
  }
}
