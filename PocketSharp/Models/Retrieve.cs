using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  [DataContract]
  class Retrieve : ResponseBase
  {
    [DataMember]
    public bool Complete { get; set; }

    [DataMember]
    public int Since { get; set; }

    [DataMember(Name = "list")]
    public List<PocketItem> Items { get; set; }
  }
}
