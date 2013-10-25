using Newtonsoft.Json;

namespace PocketSharp.Models
{
  /// <summary>
  /// Add Response
  /// </summary>
  [JsonObject]
  internal class Add : ResponseBase
  {
    /// <summary>
    /// Gets or sets the item.
    /// </summary>
    /// <value>
    /// The item.
    /// </value>
    [JsonProperty]
    public PocketItem Item { get; set; }
  }
}
