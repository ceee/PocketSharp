using Newtonsoft.Json;

namespace PocketSharp.Models
{
  /// <summary>
  /// Base for Responses
  /// </summary>
  [JsonObject]
  internal class ResponseBase
  {
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="ResponseBase"/> is status.
    /// </summary>
    /// <value>
    ///   <c>true</c> if status is OK; otherwise, <c>false</c>.
    /// </value>
    [JsonProperty("status")]
    public bool Status { get; set; }
  }
}
