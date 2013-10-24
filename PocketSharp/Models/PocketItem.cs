using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using PropertyChanged;

namespace PocketSharp.Models
{
  /// <summary>
  /// Item containing all available data
  /// see: http://getpocket.com/developer/docs/v3/retrieve
  /// </summary>
  [JsonObject]
  [ImplementPropertyChanged]
  public class PocketItem
  {
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    /// <value>
    /// The ID.
    /// </value>
    [JsonProperty("item_id")]
    public int ID { get; set; }

    /// <summary>
    /// Gets or sets the URI.
    /// </summary>
    /// <value>
    /// The URI.
    /// </value>
    [JsonProperty("resolved_url")]
    public Uri Uri { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    [JsonProperty("resolved_title")]
    private string _ResolvedTitle { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    [JsonProperty("title")]
    private string _InternalTitle { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    [JsonIgnore]
    public string Title
    {
      get { return _InternalTitle ?? _ResolvedTitle; }
      set { _InternalTitle = value; _ResolvedTitle = value; }
    }

    /// <summary>
    /// Gets or sets the full title.
    /// </summary>
    /// <value>
    /// The full title.
    /// </value>
    [JsonProperty("given_title")]
    public string FullTitle { get; set; }

    /// <summary>
    /// Gets or sets the excerpt.
    /// </summary>
    /// <value>
    /// The excerpt.
    /// </value>
    [JsonProperty]
    public string Excerpt { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    /// <value>
    /// The status.
    /// </value>
    [JsonProperty]
    public int Status { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is favorite.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is favorite; otherwise, <c>false</c>.
    /// </value>
    [JsonProperty("favorite")]
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Gets a value indicating whether this instance is archive.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is archive; otherwise, <c>false</c>.
    /// </value>
    [JsonIgnore]
    public bool IsArchive { get { return Status == 1; } }

    /// <summary>
    /// Gets a value indicating whether this instance is deleted.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
    /// </value>
    [JsonIgnore]
    public bool IsDeleted { get { return Status == 2; } }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is article.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is article; otherwise, <c>false</c>.
    /// </value>
    [JsonProperty("is_article")]
    public bool IsArticle { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance has image.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance has image; otherwise, <c>false</c>.
    /// </value>
    [JsonProperty("has_image")]
    public bool HasImage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance has video.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance has video; otherwise, <c>false</c>.
    /// </value>
    [JsonProperty("has_video")]
    public bool HasVideo { get; set; }

    /// <summary>
    /// Gets or sets the word count.
    /// </summary>
    /// <value>
    /// The word count.
    /// </value>
    [JsonProperty("word_count")]
    public int WordCount { get; set; }

    /// <summary>
    /// Gets or sets the sort.
    /// </summary>
    /// <value>
    /// The sort.
    /// </value>
    [JsonProperty("sort_id")]
    public int Sort { get; set; }


    /// <summary>
    /// Gets or sets the add time.
    /// </summary>
    /// <value>
    /// The add time.
    /// </value>
    [JsonProperty("time_added")]
    public DateTime? AddTime { get; set; }

    /// <summary>
    /// Gets or sets the update time.
    /// </summary>
    /// <value>
    /// The update time.
    /// </value>
    [JsonProperty("time_updated")]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// Gets or sets the read time.
    /// </summary>
    /// <value>
    /// The read time.
    /// </value>
    [JsonProperty("time_read")]
    public DateTime? ReadTime { get; set; }

    /// <summary>
    /// Gets or sets the favorite time.
    /// </summary>
    /// <value>
    /// The favorite time.
    /// </value>
    [JsonProperty("time_favorited")]
    public DateTime? FavoriteTime { get; set; }


    /// <summary>
    /// Gets or sets the _ tag dictionary.
    /// </summary>
    /// <value>
    /// The _ tag dictionary.
    /// </value>
    [JsonProperty("tags")]
    private Dictionary<string, PocketTag> _TagDictionary { get; set; }

    /// <summary>
    /// Gets or sets the _ image dictionary.
    /// </summary>
    /// <value>
    /// The _ image dictionary.
    /// </value>
    [JsonProperty("images")]
    private Dictionary<string, PocketImage> _ImageDictionary { get; set; }

    /// <summary>
    /// Gets or sets the _ video dictionary.
    /// </summary>
    /// <value>
    /// The _ video dictionary.
    /// </value>
    [JsonProperty("videos")]
    private Dictionary<string, PocketVideo> _VideoDictionary { get; set; }

    /// <summary>
    /// Gets or sets the _ author dictionary.
    /// </summary>
    /// <value>
    /// The _ author dictionary.
    /// </value>
    [JsonProperty("authors")]
    private Dictionary<string, PocketAuthor> _AuthorDictionary { get; set; }


    /// <summary>
    /// Gets the tags.
    /// </summary>
    /// <value>
    /// The tags.
    /// </value>
    [JsonIgnore]
    public List<PocketTag> Tags
    {
      get { return Utilities.DictionaryToList<PocketTag>(_TagDictionary); }
    }

    /// <summary>
    /// Gets the images.
    /// </summary>
    /// <value>
    /// The images.
    /// </value>
    [JsonIgnore]
    public List<PocketImage> Images
    {
      get { return Utilities.DictionaryToList<PocketImage>(_ImageDictionary); }
    }

    /// <summary>
    /// Gets the lead image.
    /// </summary>
    /// <value>
    /// The lead image.
    /// </value>
    [JsonIgnore]
    public PocketImage LeadImage
    {
      get { return Images != null && Images.Count > 0 ? Images[0] : null; }
    }

    /// <summary>
    /// Gets the videos.
    /// </summary>
    /// <value>
    /// The videos.
    /// </value>
    [JsonIgnore]
    public List<PocketVideo> Videos
    {
      get { return Utilities.DictionaryToList<PocketVideo>(_VideoDictionary); }
    }

    /// <summary>
    /// Gets the authors.
    /// </summary>
    /// <value>
    /// The authors.
    /// </value>
    [JsonIgnore]
    public List<PocketAuthor> Authors
    {
      get { return Utilities.DictionaryToList<PocketAuthor>(_AuthorDictionary); }
    }
  }
}
