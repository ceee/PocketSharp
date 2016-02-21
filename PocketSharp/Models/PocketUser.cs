using Newtonsoft.Json;

namespace PocketSharp.Models
{
  /// <summary>
  /// Access Code
  /// </summary>
  [JsonObject]
  public class PocketUser
  {
    /// <summary>
    /// Gets or sets the code.
    /// </summary>
    /// <value>
    /// The code.
    /// </value>
    [JsonProperty("access_token")]
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    /// <value>
    /// The username.
    /// </value>
    [JsonProperty("username")]
    public string Username { get; set; }
  }
}
