using PocketSharp.Models;
using System.Collections.Generic;

namespace PocketSharp
{
  /// <summary>
  /// PocketClient
  /// </summary>
  public partial class PocketClient
  {
    /// <summary>
    /// Adds the specified tags.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    public bool AddTags(int itemID, string[] tags)
    {
      return PutSendActionForTags(itemID, "tags_add", tags);
    }


    /// <summary>
    /// Removes the specified tags.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    public bool RemoveTags(int itemID, string[] tags)
    {
      return PutSendActionForTags(itemID, "tags_remove", tags);
    }


    /// <summary>
    /// Clears all tags.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    public bool RemoveTags(int itemID)
    {
      return PutSendActionDefault(itemID, "tags_clear");
    }


    /// <summary>
    /// Replaces the specified tags.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    public bool ReplaceTags(int itemID, string[] tags)
    {
      return PutSendActionForTags(itemID, "tags_replace", tags);
    }


    /// <summary>
    /// Renames a tag.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="oldTag">The old tag.</param>
    /// <param name="newTag">The new tag name.</param>
    /// <returns></returns>
    public bool RenameTag(int itemID, string oldTag, string newTag)
    {
      return PutSendAction(new ActionParameter()
      {
        Action = "tag_rename",
        ID = itemID,
        OldTag = oldTag,
        NewTag = newTag
      });
    }



    /// <summary>
    /// Puts the send action for tags.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    protected bool PutSendActionForTags(int itemID, string action, string[] tags)
    {
      return PutSendAction(new ActionParameter()
      {
        Action = action,
        ID = itemID,
        Tags = tags
      });
    }
  }
}
