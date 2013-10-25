using Newtonsoft.Json;
using System.Collections.Generic;

namespace PocketSharp.Models
{
  /// <summary>
  /// Item Response
  /// </summary>
  [JsonObject]
  internal class Retrieve : ResponseBase
  {
    /// <summary>
    /// Gets or sets the complete.
    /// </summary>
    /// <value>
    /// The complete.
    /// </value>
    [JsonProperty("complete")]
    public int Complete { get; set; }

    /// <summary>
    /// Gets or sets the since.
    /// </summary>
    /// <value>
    /// The since.
    /// </value>
    [JsonProperty]
    public int Since { get; set; }

    /// <summary>
    /// Gets or sets the _ item dictionary.
    /// </summary>
    /// <value>
    /// The _ item dictionary.
    /// </value>
    [JsonProperty("list")]
    public Dictionary<string, PocketItem> ItemDictionary { get; set; }

    /// <summary>
    /// Gets the items.
    /// </summary>
    /// <value>
    /// The items.
    /// </value>
    [JsonIgnore]
    public List<PocketItem> Items
    {
      get { return Utilities.DictionaryToList<PocketItem>(ItemDictionary); }
    }
  }
}
