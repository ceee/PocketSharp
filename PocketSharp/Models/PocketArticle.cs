using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PocketSharp.Models
{
  /// <summary>
  /// Article
  /// </summary>
  [JsonObject]
  public class PocketArticle
  {
    /// <summary>
    /// Gets or sets the response Code (200 = OK).
    /// </summary>
    /// <value>
    /// The response Code.
    /// </value>
    [JsonProperty("responseCode")]
    public string ResponseCode { get; set; }

    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    /// <value>
    /// The ID.
    /// </value>
    [JsonProperty("resolved_id")]
    public string ID { get; set; }

    /// <summary>
    /// Gets or sets the URI.
    /// </summary>
    /// <value>
    /// The URI.
    /// </value>
    [JsonProperty("resolvedUrl")]
    public Uri Uri { get; set; }

    /// <summary>
    /// Gets or sets the URI.
    /// </summary>
    /// <value>
    /// The URI.
    /// </value>
    [JsonProperty("timePublished")]
    public DateTime? PublishedTime { get; set; }

    /// <summary>
    /// Gets or sets the word count.
    /// </summary>
    /// <value>
    /// The word count.
    /// </value>
    [JsonProperty("wordCount")]
    public int WordCount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is article.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is article; otherwise, <c>false</c>.
    /// </value>
    [JsonProperty("isArticle")]
    public bool IsArticle { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is video.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is video; otherwise, <c>false</c>.
    /// </value>
    [JsonProperty("isVideo")]
    public bool IsVideo { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is index.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is index; otherwise, <c>false</c>.
    /// </value>
    [JsonProperty("isIndex")]
    public bool IsIndex { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance used fallback.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance used fallback; otherwise, <c>false</c>.
    /// </value>
    [JsonProperty("usedFallback")]
    public bool UsedFallback { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance requires login.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance requires login; otherwise, <c>false</c>.
    /// </value>
    [JsonProperty("requiresLogin")]
    public bool RequiresLogin { get; set; }

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
    /// Gets or sets the article title.
    /// </summary>
    /// <value>
    /// The Title.
    /// </value>
    [JsonProperty("title")]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the Excerpt.
    /// </summary>
    /// <value>
    /// The Excerpt.
    /// </value>
    [JsonProperty("excerpt")]
    public string Excerpt { get; set; }

    /// <summary>
    /// Gets or sets the Content.
    /// </summary>
    /// <value>
    /// The Content.
    /// </value>
    [JsonProperty("article")]
    public string Content { get; set; }
  }
}
