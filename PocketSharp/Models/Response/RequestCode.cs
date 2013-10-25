using Newtonsoft.Json;

namespace PocketSharp.Models
{
  /// <summary>
  /// Request Code
  /// </summary>
  [JsonObject]
  internal class RequestCode
  {
    /// <summary>
    /// Gets or sets the code.
    /// </summary>
    /// <value>
    /// The code.
    /// </value>
    [JsonProperty]
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    /// <value>
    /// The state.
    /// </value>
    [JsonProperty]
    public string State { get; set; }
  }
}
