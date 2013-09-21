using PocketSharp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PocketSharp
{
  /// <summary>
  /// PocketClient
  /// </summary>
  public partial class PocketClient
  {
    /// <summary>
    /// Adds the specified tags to an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> AddTags(int itemID, string[] tags)
    {
      return await SendTags(itemID, "tags_add", tags);
    }


    /// <summary>
    /// Adds the specified tags to an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> AddTags(PocketItem item, string[] tags)
    {
      return await AddTags(item.ID, tags);
    }


    /// <summary>
    /// Removes the specified tags from an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTags(int itemID, string[] tags)
    {
      return await SendTags(itemID, "tags_remove", tags);
    }


    /// <summary>
    /// Removes the specified tags from an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="tags">The tag.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTags(PocketItem item, string[] tags)
    {
      return await RemoveTags(item.ID, tags);
    }


    /// <summary>
    /// Removes a tag from an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tag.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTag(int itemID, string tag)
    {
      return await SendTags(itemID, "tags_remove", new string[] { tag });
    }


    /// <summary>
    /// Removes a tag from an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTag(PocketItem item, string tag)
    {
      return await RemoveTag(item.ID, tag);
    }


    /// <summary>
    /// Clears all tags from an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTags(int itemID)
    {
      return await SendDefault(itemID, "tags_clear");
    }


    /// <summary>
    /// Clears all tags from an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTags(PocketItem item)
    {
      return await RemoveTags(item.ID);
    }


    /// <summary>
    /// Replaces all existing tags with the given tags in an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> ReplaceTags(int itemID, string[] tags)
    {
      return await SendTags(itemID, "tags_replace", tags);
    }


    /// <summary>
    /// Replaces all existing tags with the given new ones in an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> ReplaceTags(PocketItem item, string[] tags)
    {
      return await ReplaceTags(item.ID, tags);
    }


    /// <summary>
    /// Renames a tag in an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="oldTag">The old tag.</param>
    /// <param name="newTag">The new tag name.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RenameTag(int itemID, string oldTag, string newTag)
    {
      return await Send(new ActionParameter()
      {
        Action = "tag_rename",
        ID = itemID,
        OldTag = oldTag,
        NewTag = newTag
      });
    }


    /// <summary>
    /// Renames a tag in an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="oldTag">The old tag.</param>
    /// <param name="newTag">The new tag name.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RenameTag(PocketItem item, string oldTag, string newTag)
    {
      return await RenameTag(item.ID, oldTag, newTag);
    }


    /// <summary>
    /// Puts the send action for tags.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="action">The action.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    protected async Task<bool> SendTags(int itemID, string action, string[] tags)
    {
      return await Send(new ActionParameter()
      {
        Action = action,
        ID = itemID,
        Tags = tags
      });
    }
  }
}
