using System;
using System.Collections.Generic;
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

    [DataMember]
    public int Status { get; set; }

    [DataMember(Name = "favorite")]
    public bool IsFavorite { get; set; }

    [IgnoreDataMember]
    public bool IsArchive { get { return Status == 1; } }

    [IgnoreDataMember]
    public bool IsDeleted { get { return Status == 2; } }

    [DataMember(Name = "is_article")]
    public bool IsArticle { get; set; }

    [DataMember(Name = "has_image")]
    public bool HasImage { get; set; }

    [DataMember(Name = "has_video")]
    public bool HasVideo { get; set; }

    [DataMember(Name = "word_count")]
    public int WordCount { get; set; }

    [DataMember(Name = "sort_id")]
    public int Sort { get; set; }


    [DataMember(Name = "time_added")]
    public DateTime? AddTime { get; set; }

    [DataMember(Name = "time_updated")]
    public DateTime? UpdateTime { get; set; }

    [DataMember(Name = "time_read")]
    public DateTime? ReadTime { get; set; }

    [DataMember(Name = "time_favorited")]
    public DateTime? FavoriteTime { get; set; }


    [DataMember(Name = "tags")]
    public Dictionary<string, PocketTag> _TagDictionary { get; set; }

    [DataMember(Name = "images")]
    public Dictionary<string, PocketImage> _ImageDictionary { get; set; }

    [DataMember(Name = "videos")]
    public Dictionary<string, PocketVideo> _VideoDictionary { get; set; }

    [DataMember(Name = "authors")]
    public Dictionary<string, PocketAuthor> _AuthorDictionary { get; set; }


    [IgnoreDataMember]
    public List<PocketTag> Tags
    {
      get { return PocketClient.DictionaryToList<PocketTag>(_TagDictionary); }
    }

    [IgnoreDataMember]
    public List<PocketImage> Images
    {
      get { return PocketClient.DictionaryToList<PocketImage>(_ImageDictionary); }
    }

    [IgnoreDataMember]
    public PocketImage LeadImage
    {
      get { return Images != null ? Images[0] : null; }
    }

    [IgnoreDataMember]
    public List<PocketVideo> Videos
    {
      get { return PocketClient.DictionaryToList<PocketVideo>(_VideoDictionary); }
    }

    [IgnoreDataMember]
    public List<PocketAuthor> Authors
    {
      get { return PocketClient.DictionaryToList<PocketAuthor>(_AuthorDictionary); }
    }
  }
}
