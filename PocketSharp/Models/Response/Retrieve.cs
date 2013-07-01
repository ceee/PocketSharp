using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  /// <summary>
  /// Item Response
  /// </summary>
  [DataContract]
  internal class Retrieve : ResponseBase
  {
    /// <summary>
    /// Gets or sets the complete.
    /// </summary>
    /// <value>
    /// The complete.
    /// </value>
    [DataMember(Name = "complete")]
    public int Complete { get; set; }

    /// <summary>
    /// Gets or sets the since.
    /// </summary>
    /// <value>
    /// The since.
    /// </value>
    [DataMember]
    public int Since { get; set; }

    /// <summary>
    /// Gets or sets the _ item dictionary.
    /// </summary>
    /// <value>
    /// The _ item dictionary.
    /// </value>
    [DataMember(Name = "list")]
    public Dictionary<string, PocketItem> _ItemDictionary { get; set; }

    /// <summary>
    /// Gets the items.
    /// </summary>
    /// <value>
    /// The items.
    /// </value>
    [IgnoreDataMember]
    public List<PocketItem> Items
    {
      get { return Utilities.DictionaryToList<PocketItem>(_ItemDictionary); }
    }
  }
}
