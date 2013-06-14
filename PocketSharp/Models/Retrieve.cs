using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  [DataContract]
  class Retrieve : ResponseBase
  {
    [DataMember(Name = "complete")]
    public int Complete;

    [DataMember]
    public int Since { get; set; }

    [DataMember(Name = "list")]
    public Dictionary<string, PocketItem> ItemDictionary { get; set; }

    [IgnoreDataMember]
    public List<PocketItem> Items
    {
      get
      {
        var itemEnumerator = ItemDictionary.GetEnumerator();
        List<PocketItem> result = new List<PocketItem>();

        while (itemEnumerator.MoveNext())
        {
          result.Add(itemEnumerator.Current.Value);
        }

        return result;
      }
      private set {}
    }
  }
}
