using System;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  [DataContract]
  public class PocketVideo
  {
    [DataMember(Name = "video_id")]
    public string ID { get; set; }

    [DataMember(Name = "vid")]
    public string ExternalID { get; set; }

    [DataMember(Name = "src")]
    public Uri Uri { get; set; }
  }
}
