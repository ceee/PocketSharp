using System;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  [DataContract]
  class PocketItem
  {
    [DataMember(Name = "item_id")]
    public int ID { get; set; }

    private Uri _uri;
    [DataMember(Name = "resolved_url")]
    public Uri Uri
    {
      get { return _uri; }
      set { _uri = new Uri(value.ToString()); }
    }

    [DataMember(Name = "resolved_title")]
    public string Title { get; set; }

    [DataMember(Name = "given_title")]
    public string FullTitle { get; set; }

    [DataMember]
    public string Excerpt { get; set; }
  }
}
