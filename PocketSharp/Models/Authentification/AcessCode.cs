using RestSharp.Serializers;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketSharp.Models.Authentification
{
  [DataContract]
  public class AccessCode
  {
    [DataMember(Name = "access_token")]
    public string Code { get; set; }

    [DataMember]
    public string Username { get; set; }
  }
}
