using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PocketSharp.Models
{
  /// <summary>
  /// Item containing all available data
  /// see: http://getpocket.com/developer/docs/v3/retrieve
  /// </summary>
  [JsonObject]
  [ImplementPropertyChanged]
  [DebuggerDisplay("Uri = {Uri}, Title = {Title}")]

  public class PocketItem
  {
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    /// <value>
    /// The ID.
    /// </value>
    [JsonProperty("item_id")]
    public string ID { get; set; }

    /// <summary>
    /// Gets or sets the resolved identifier.
    /// </summary>
    /// <value>
    /// The resolved identifier.
    /// </value>
    [JsonProperty("resolved_id")]
    public string ResolvedId { get; set; }

    /// <summary>
    /// Gets or sets the normal URI.
    /// </summary>
    /// <value>
    /// The normal URI.
    /// </value>
    [JsonProperty("normal_url")]
    private Uri _NormalUri { get; set; }

    /// <summary>
    /// Gets or sets the given URI.
    /// </summary>
    /// <value>
    /// The given URI.
    /// </value>
    [JsonProperty("given_url")]
    private Uri _GivenUri { get; set; }

    /// <summary>
    /// Gets or sets the resolved URI.
    /// </summary>
    /// <value>
    /// The resolved URI.
    /// </value>
    [JsonProperty("resolved_url")]
    private Uri _ResolvedUri { get; set; }

    /// <summary>
    /// Gets or sets the URI.
    /// </summary>
    /// <value>
    /// The URI.
    /// </value>
    [JsonIgnore]
    public Uri Uri
    {
      get { return _ResolvedUri ?? _GivenUri ?? _NormalUri; }
      set { _NormalUri = value; _ResolvedUri = value; _GivenUri = value; }
    }

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
      get { return _InternalTitle ?? _ResolvedTitle ?? FullTitle; }
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
    private PocketBoolean? _HasImage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance has video.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance has video; otherwise, <c>false</c>.
    /// </value>
    [JsonProperty("has_video")]
    private PocketBoolean? _HasVideo { get; set; }

    /// <summary>
    /// Gets a value indicating whether [has image].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [has image]; otherwise, <c>false</c>.
    /// </value>
    [JsonIgnore]
    public bool HasImage
    {
      get
      {
        return _HasImage == PocketBoolean.Yes || _HasImage == PocketBoolean.IsType;
      }
    }

    /// <summary>
    /// Gets a value indicating whether [has video].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [has video]; otherwise, <c>false</c>.
    /// </value>
    [JsonIgnore]
    public bool HasVideo
    {
      get
      {
        return _HasVideo == PocketBoolean.Yes || _HasVideo == PocketBoolean.IsType;
      }
    }

    /// <summary>
    /// Gets a value indicating whether [is video].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [is video]; otherwise, <c>false</c>.
    /// </value>
    [JsonIgnore]
    public bool IsVideo
    {
      get
      {
        return _HasVideo == PocketBoolean.IsType;
      }
    }

    /// <summary>
    /// Gets a value indicating whether [is image].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [is image]; otherwise, <c>false</c>.
    /// </value>
    [JsonIgnore]
    public bool IsImage
    {
      get
      {
        return _HasImage == PocketBoolean.IsType;
      }
    }

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
    /// Gets or sets the tags as comma-separated strings.
    /// </summary>
    /// <value>
    /// The tags.
    /// </value>
    [JsonIgnore]
    public string TagsString
    {
      get { return Tags != null ? String.Join(",", Tags.Select(tag => tag.Name)) : ""; }
    }

    /// <summary>
    /// Gets or sets the tags.
    /// </summary>
    /// <value>
    /// The tags.
    /// </value>
    [JsonProperty("tags")]
    [JsonConverter(typeof(ObjectToArrayConverter<PocketTag>))]
    public IEnumerable<PocketTag> Tags { get; set; }

    /// <summary>
    /// Gets or sets the images.
    /// </summary>
    /// <value>
    /// The images.
    /// </value>
    [JsonProperty("images")]
    [JsonConverter(typeof(ObjectToArrayConverter<PocketImage>))]
    public IEnumerable<PocketImage> Images { get; set; }

    /// <summary>
    /// Gets or sets the videos.
    /// </summary>
    /// <value>
    /// The videos.
    /// </value>
    [JsonProperty("videos")]
    [JsonConverter(typeof(ObjectToArrayConverter<PocketVideo>))]
    public IEnumerable<PocketVideo> Videos { get; set; }

    /// <summary>
    /// Gets or sets the authors.
    /// </summary>
    /// <value>
    /// The authors.
    /// </value>
    [JsonProperty("authors")]
    [JsonConverter(typeof(ObjectToArrayConverter<PocketAuthor>))]
    public IEnumerable<PocketAuthor> Authors { get; set; }

    /// <summary>
    /// Gets the lead image.
    /// </summary>
    /// <value>
    /// The lead image.
    /// </value>
    [JsonIgnore]
    public PocketImage LeadImage
    {
      get { return Images != null && Images.Count() > 0 ? Images.First() : null; }
    }

    /// <summary>
    /// Gets and sets the JSON the model was deserialized from
    /// </summary>
    /// <value>
    /// Model's original JSON representation
    /// </value>
    [JsonIgnore]
    public string Json { get; set; }
  }
}
