using System;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  [DataContract]
  public class PocketAuthor
  {
    [DataMember(Name = "author_id")]
    public string ID { get; set; }

    [DataMember]
    public string Name { get; set; }

    [DataMember(Name = "url")]
    public Uri Uri { get; set; }
  }
}
