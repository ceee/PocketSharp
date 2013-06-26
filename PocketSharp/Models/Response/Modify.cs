using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  [DataContract]
  class Modify : ResponseBase
  {
    [DataMember(Name = "action_results")]
    public bool[] ActionResults { get; set; }
  }
}
