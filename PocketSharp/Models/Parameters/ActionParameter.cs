using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  /// <summary>
  /// All parameters which can be passed for a modify action
  /// </summary>
  [DataContract]
  internal class ActionParameter : Parameters
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
    /// Gets or sets the ID.
    /// </summary>
    /// <value>
    /// The ID.
    /// </value>
    [DataMember(Name = "item_id")]
    public int ID { get; set; }

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


    ///// <summary>
    ///// Converts this instance to a parameter list.
    ///// </summary>
    ///// <returns></returns>
    //public Dictionary<string, object> Convert()
    //{
    //  Dictionary<string, object> parameters = new Dictionary<string, object>
    //  {
    //    { "item_id", ID },
    //    { "action", Action }
    //  };

    //  if (Time != null)
    //    parameters.Add("time", Utilities.GetUnixTimestamp(Time));
    //  if (Tags != null)
    //    parameters.Add("tags", Tags);
    //  if (OldTag != null)
    //    parameters.Add("old_tag", OldTag);
    //  if (NewTag != null)
    //    parameters.Add("new_tag", NewTag);

    //  return parameters;
    //}
  }
}
