using RestSharp;
using System;
using System.Collections.Generic;

namespace PocketSharp.Models
{
  /// <summary>
  /// All parameters which can be passed for item retrieval
  /// </summary>
  public class RetrieveParameters
  {
    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    /// <value>
    /// The state.
    /// </value>
    public State? State { get; set; }

    /// <summary>
    /// Gets or sets the favorite.
    /// </summary>
    /// <value>
    /// The favorite.
    /// </value>
    public bool? Favorite { get; set; }

    /// <summary>
    /// Gets or sets the tag.
    /// </summary>
    /// <value>
    /// The tag.
    /// </value>
    public string Tag { get; set; }

    /// <summary>
    /// Gets or sets the type of the content.
    /// </summary>
    /// <value>
    /// The type of the content.
    /// </value>
    public ContentType? ContentType { get; set; }

    /// <summary>
    /// Gets or sets the sort.
    /// </summary>
    /// <value>
    /// The sort.
    /// </value>
    public Sort? Sort { get; set; }

    /// <summary>
    /// Gets or sets the type of the detail.
    /// </summary>
    /// <value>
    /// The type of the detail.
    /// </value>
    public DetailType? DetailType { get; set; }

    /// <summary>
    /// Gets or sets the search.
    /// </summary>
    /// <value>
    /// The search.
    /// </value>
    public string Search { get; set; }

    /// <summary>
    /// Gets or sets the domain.
    /// </summary>
    /// <value>
    /// The domain.
    /// </value>
    public string Domain { get; set; }

    /// <summary>
    /// Gets or sets the since.
    /// </summary>
    /// <value>
    /// The since.
    /// </value>
    public DateTime? Since { get; set; }

    /// <summary>
    /// Gets or sets the count.
    /// </summary>
    /// <value>
    /// The count.
    /// </value>
    public int? Count { get; set; }

    /// <summary>
    /// Gets or sets the offset.
    /// </summary>
    /// <value>
    /// The offset.
    /// </value>
    public int? Offset { get; set; }


    /// <summary>
    /// Converts this instance to a parameter list.
    /// </summary>
    /// <returns></returns>
    public List<Parameter> Convert()
    {
      return new List<Parameter>()
      {
        Utilities.CreateParam("state", State != null ? State.ToString() : null ),
        Utilities.CreateParam("favorite", Favorite != null ? (bool)Favorite ? "1" : "0" : null),
        Utilities.CreateParam("tag", Tag),
        Utilities.CreateParam("contentType", ContentType != null ? ContentType.ToString() : null),
        Utilities.CreateParam("sort", Sort != null ? Sort.ToString() : null),
        Utilities.CreateParam("detailType", DetailType != null ? DetailType.ToString() : null),
        Utilities.CreateParam("search", Search),
        Utilities.CreateParam("domain", Domain),
        Utilities.CreateParam("since", Utilities.GetUnixTimestamp(Since)),
        Utilities.CreateParam("count", Count),
        Utilities.CreateParam("offset", Offset)
      };
    }
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
