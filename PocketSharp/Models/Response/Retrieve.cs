using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  [DataContract]
  class Retrieve : ResponseBase
  {
    [DataMember(Name = "complete")]
    public int Complete { get; set; }

    [DataMember]
    public int Since { get; set; }

    [DataMember(Name = "list")]
    public Dictionary<string, PocketItem> _ItemDictionary { get; set; }

    [IgnoreDataMember]
    public List<PocketItem> Items
    {
      get { return PocketClient.DictionaryToList<PocketItem>(_ItemDictionary); }
    }
  }
}
