using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  /// <summary>
  /// All parameters which can be passed for a send action
  /// </summary>
  [DataContract]
  public class PocketAction
  {
    /// <summary>
    /// Gets or sets the action.
    /// </summary>
    /// <value>
    /// The action.
    /// </value>
    [DataMember(Name = "action")]
    public string Action { get; set; }

    /// <summary>
    /// Gets or sets the PocketItem ID.
    /// </summary>
    /// <value>
    /// The ID.
    /// </value>
    [DataMember(Name = "item_id")]
    public string ID { get; set; }

    /// <summary>
    /// Gets or sets the URI (for adding a new item).
    /// </summary>
    /// <value>
    /// The URI.
    /// </value>
    [DataMember(Name = "url")]
    public Uri Uri { get; set; }

    /// <summary>
    /// Gets or sets the Title (for adding a new item).
    /// </summary>
    /// <value>
    /// The Title.
    /// </value>
    [DataMember(Name = "title")]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the associated Tweet ID.
    /// </summary>
    /// <value>
    /// The Tweet ID.
    /// </value>
    [DataMember(Name = "ref_id")]
    public string TweetID { get; set; }

    /// <summary>
    /// Gets or sets the time.
    /// </summary>
    /// <value>
    /// The time.
    /// </value>
    [DataMember(Name = "time")]
    public DateTime? Time { get; set; }

    // specific params

    /// <summary>
    /// Gets or sets the tags.
    /// </summary>
    /// <value>
    /// The tags.
    /// </value>
    [DataMember(Name = "tags")]
    public string[] Tags { get; set; }

    /// <summary>
    /// Gets or sets the old tag.
    /// </summary>
    /// <value>
    /// The old tag.
    /// </value>
    [DataMember(Name = "old_tag")]
    public string OldTag { get; set; }

    /// <summary>
    /// Gets or sets the new tag.
    /// </summary>
    /// <value>
    /// The new tag.
    /// </value>
    [DataMember(Name = "new_tag")]
    public string NewTag { get; set; }


    /// <summary>
    /// Converts this instance to a parameter list.
    /// </summary>
    /// <returns></returns>
    internal Dictionary<string, object> Convert()
    {
      Dictionary<string, object> parameters = new Dictionary<string, object>
      {
        { "action", Action }
      };

      if (!String.IsNullOrEmpty(ID) && ID != "0")
        parameters.Add("item_id", ID.ToString());
      if (Time != null)
        parameters.Add("time", Time != null ? Utilities.GetUnixTimestamp(Time).ToString() : null);
      if (Tags != null)
        parameters.Add("tags", Tags);
      if (OldTag != null)
        parameters.Add("old_tag", OldTag);
      if (NewTag != null)
        parameters.Add("new_tag", NewTag);
      if (!String.IsNullOrEmpty(Title))
        parameters.Add("title", Title);
      if (!String.IsNullOrEmpty(TweetID))
        parameters.Add("ref_id", TweetID);
      if (Uri != null)
        parameters.Add("url", Uri.OriginalString);

      return parameters;
    }
  }
}
