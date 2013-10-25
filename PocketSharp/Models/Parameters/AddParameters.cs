using System;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  /// <summary>
  /// All parameters which can be passed to add a new item
  /// </summary>
  [DataContract]
  internal class AddParameters : Parameters
  {
    /// <summary>
    /// Gets or sets the URI.
    /// </summary>
    /// <value>
    /// The URI.
    /// </value>
    [DataMember(Name="url")]
    public Uri Uri { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    [DataMember(Name="title")]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the tags.
    /// </summary>
    /// <value>
    /// The tags.
    /// </value>
    [DataMember(Name="tags")]
    public string[] Tags { get; set; }

    /// <summary>
    /// Gets or sets the tweet ID.
    /// </summary>
    /// <value>
    /// The tweet ID.
    /// </value>
    [DataMember(Name="tweet_id")]
    public string TweetID { get; set; }
  }
}
