using Newtonsoft.Json;
using PropertyChanged;

namespace PocketSharp.Models
{
  /// <summary>
  /// Tag
  /// </summary>
  [JsonObject]
  [ImplementPropertyChanged]
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
