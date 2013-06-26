using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  [DataContract]
  class Add : ResponseBase
  {
    [DataMember]
    public PocketItem Item { get; set; }
  }
}
