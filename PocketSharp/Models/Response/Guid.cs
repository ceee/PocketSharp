using Newtonsoft.Json;

namespace PocketSharp.Models
{
  /// <summary>
  /// Guid
  /// </summary>
  [JsonObject]
  internal class GuidResponse
  {
    /// <summary>
    /// Gets or sets the GUID.
    /// </summary>
    /// <value>
    /// The GUID.
    /// </value>
    [JsonProperty]
    public string Guid { get; set; }
  }
}
