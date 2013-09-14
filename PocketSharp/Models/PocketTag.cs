using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  /// <summary>
  /// Tag
  /// </summary>
  [JsonObject]
  public class PocketTag
  {
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [JsonProperty("tag")]
    public string Name { get; set; }
  }
}
