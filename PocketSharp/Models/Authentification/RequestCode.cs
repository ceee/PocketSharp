using RestSharp.Serializers;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketSharp.Models.Authentification
{
  [DataContract]
  class RequestCode
  {
    [DataMember]
    public string Code { get; set; }

    [DataMember]
    public string State { get; set; }
  }
}
