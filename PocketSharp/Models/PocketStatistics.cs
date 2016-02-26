using Newtonsoft.Json;

namespace PocketSharp.Models
{
  /// <summary>
  /// Statistics
  /// </summary>
  [JsonObject]
  public class PocketStatistics
  {
    /// <summary>
    /// Gets or sets all items.
    /// </summary>
    /// <value>
    /// All items count.
    /// </value>
    [JsonProperty("count_list")]
    public int CountAll { get; set; }

    /// <summary>
    /// Gets or sets all read items.
    /// </summary>
    /// <value>
    /// Read items count.
    /// </value>
    [JsonProperty("count_read")]
    public int CountRead { get; set; }

    /// <summary>
    /// Gets or sets all unread items.
    /// </summary>
    /// <value>
    /// Unread items count.
    /// </value>
    [JsonProperty("count_unread")]
    public int CountUnread { get; set; }
  }
}
