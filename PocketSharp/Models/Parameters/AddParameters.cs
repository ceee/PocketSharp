using RestSharp;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  /// <summary>
  /// All parameters which can be passed to add a new item
  /// </summary>
  public class AddParameters
  {
    /// <summary>
    /// Gets or sets the URI.
    /// </summary>
    /// <value>
    /// The URI.
    /// </value>
    public Uri Uri { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the tags.
    /// </summary>
    /// <value>
    /// The tags.
    /// </value>
    public string[] Tags { get; set; }

    /// <summary>
    /// Gets or sets the tweet ID.
    /// </summary>
    /// <value>
    /// The tweet ID.
    /// </value>
    public string TweetID { get; set; }

    /// <summary>
    /// Converts this instance to a parameter list.
    /// </summary>
    /// <returns></returns>
    public List<Parameter> Convert()
    {
      return new List<Parameter>()
      {
        Utilities.CreateParam("url", Uri.ToString() ),
        Utilities.CreateParam("title", Title),
        Utilities.CreateParam("tags", String.Join(",", Tags)),
        Utilities.CreateParam("tweet_id", TweetID)
      };
    }
  }
}
