using System;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  /// <summary>
  /// All parameters which can be passed for item retrieval
  /// </summary>
  [DataContract]
  internal class RetrieveParameters : Parameters
  {
    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    /// <value>
    /// The state.
    /// </value>
    [DataMember(Name = "state")]
    public State? State { get; set; }

    /// <summary>
    /// Gets or sets the favorite.
    /// </summary>
    /// <value>
    /// The favorite.
    /// </value>
    [DataMember(Name = "favorite")]
    public bool? Favorite { get; set; }

    /// <summary>
    /// Gets or sets the tag.
    /// </summary>
    /// <value>
    /// The tag.
    /// </value>
    [DataMember(Name = "tag")]
    public string Tag { get; set; }

    /// <summary>
    /// Gets or sets the type of the content.
    /// </summary>
    /// <value>
    /// The type of the content.
    /// </value>
    [DataMember(Name = "contentType")]
    public ContentType? ContentType { get; set; }

    /// <summary>
    /// Gets or sets the sort.
    /// </summary>
    /// <value>
    /// The sort.
    /// </value>
    [DataMember(Name = "sort")]
    public Sort? Sort { get; set; }

    /// <summary>
    /// Gets or sets the type of the detail.
    /// </summary>
    /// <value>
    /// The type of the detail.
    /// </value>
    [DataMember(Name="detailType")]
    public DetailType? DetailType { get; set; }

    /// <summary>
    /// Gets or sets the search.
    /// </summary>
    /// <value>
    /// The search.
    /// </value>
    [DataMember(Name = "search")]
    public string Search { get; set; }

    /// <summary>
    /// Gets or sets the domain.
    /// </summary>
    /// <value>
    /// The domain.
    /// </value>
    [DataMember(Name = "domain")]
    public string Domain { get; set; }

    /// <summary>
    /// Gets or sets the since.
    /// </summary>
    /// <value>
    /// The since.
    /// </value>
    [DataMember(Name = "since")]
    public DateTime? Since { get; set; }

    /// <summary>
    /// Gets or sets the count.
    /// </summary>
    /// <value>
    /// The count.
    /// </value>
    [DataMember(Name = "count")]
    public int? Count { get; set; }

    /// <summary>
    /// Gets or sets the offset.
    /// </summary>
    /// <value>
    /// The offset.
    /// </value>
    [DataMember(Name = "offset")]
    public int? Offset { get; set; }
  }


  /// <summary>
  /// Item states
  /// </summary>
  public enum State
  {
    /// <summary>
    /// Only unread items
    /// </summary>
    unread,
    /// <summary>
    /// Only archived items
    /// </summary>
    archive,
    /// <summary>
    /// All items
    /// </summary>
    all
  }

  /// <summary>
  /// Sorting
  /// </summary>
  public enum Sort
  {
    /// <summary>
    /// Newest first
    /// </summary>
    newest,
    /// <summary>
    /// Oldest first
    /// </summary>
    oldest,
    /// <summary>
    /// Title A-Z descending
    /// </summary>
    title,
    /// <summary>
    /// URL descending
    /// </summary>
    site
  }

  /// <summary>
  /// Item types
  /// </summary>
  public enum ContentType
  {
    /// <summary>
    /// Articles
    /// </summary>
    article,
    /// <summary>
    /// Videos
    /// </summary>
    video,
    /// <summary>
    /// Images
    /// </summary>
    image
  }

  /// <summary>
  /// Item data
  /// </summary>
  public enum DetailType
  {
    /// <summary>
    /// Necessary data
    /// </summary>
    simple,
    /// <summary>
    /// Includes all Images/videos/tags/authors
    /// </summary>
    complete
  }
}
