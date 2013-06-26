using System;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  [DataContract]
  public class PocketItem
  {
    [DataMember(Name = "item_id")]
    public int ID { get; set; }

    [DataMember(Name = "resolved_url")]
    public Uri Uri { get; set; }

    [DataMember(Name = "resolved_title")]
    public string Title { get; set; }

    [DataMember(Name = "given_title")]
    public string FullTitle { get; set; }

    [DataMember]
    public string Excerpt { get; set; }

    [DataMember(Name = "favorite")]
    public bool IsFavorite { get; set; }

  }
}
