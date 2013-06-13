using RestSharp.Serializers;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketSharp.Models.Authentification
{
  class RequestCode
  {
    public string Code { get; set; }

    public string State { get; set; }
  }
}
