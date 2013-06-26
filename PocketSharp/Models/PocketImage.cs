using System;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  [DataContract]
  public class PocketImage
  {
    [DataMember(Name = "image_id")]
    public string ID { get; set; }

    [DataMember]
    public string Caption { get; set; }

    [DataMember]
    public string Credit { get; set; }

    [DataMember(Name = "src")]
    public Uri Uri { get; set; }
  }
}
