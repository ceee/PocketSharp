using Newtonsoft.Json;

namespace PocketSharp.Models
{
  /// <summary>
  /// API Limitation Statistics
  /// </summary>
  [JsonObject]
  public class PocketLimits
  {
    /// <summary>
    /// Gets or sets the rate limit.
    /// </summary>
    /// <value>
    /// Rate limit for current consumer key.
    /// </value>
    [JsonProperty("X-Limit-Key-Limit")]
    public int RateLimitForConsumerKey { get; set; }

    /// <summary>
    /// Gets or sets the remaining calls.
    /// </summary>
    /// <value>
    /// Remaining calls for current consumer key.
    /// </value>
    [JsonProperty("X-Limit-Key-Remaining")]
    public int RemainingCallsForConsumerKey { get; set; }

    /// <summary>
    /// Gets or sets the reset seconds.
    /// </summary>
    /// <value>
    /// Seconds until limit resets for current consumer key.
    /// </value>
    [JsonProperty("X-Limit-Key-Reset")]
    public int SecondsUntilLimitResetsForConsumerKey { get; set; }

    /// <summary>
    /// Gets or sets the rate limit.
    /// </summary>
    /// <value>
    /// Rate limit for current user.
    /// </value>
    [JsonProperty("X-Limit-User-Limit")]
    public int RateLimitForUser { get; set; }

    /// <summary>
    /// Gets or sets the remaining calls.
    /// </summary>
    /// <value>
    /// Remaining calls for current user.
    /// </value>
    [JsonProperty("X-Limit-User-Remaining")]
    public int RemainingCallsForUser { get; set; }

    /// <summary>
    /// Gets or sets the reset seconds.
    /// </summary>
    /// <value>
    /// Seconds until limit resets for current user.
    /// </value>
    [JsonProperty("X-Limit-User-Reset")]
    public int SecondsUntilLimitResetsForUser { get; set; }
  }
}
