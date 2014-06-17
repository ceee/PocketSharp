using Newtonsoft.Json;

namespace PocketSharp.Models
{
  /// <summary>
  /// Modify Response
  /// </summary>
  [JsonObject]
  internal class Modify : ResponseBase
  {
    /// <summary>
    /// Gets or sets the action results.
    /// </summary>
    /// <value>
    /// The action results.
    /// </value>
    //[JsonProperty("action_results")]
    //public bool[] ActionResults { get; set; }
  }
}
