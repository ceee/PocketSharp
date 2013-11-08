using PocketSharp.Models;
using System.Threading;
using System.Threading.Tasks;

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
      return await AddTags(CancellationToken.None, itemID, tags);
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
      return await AddTags(CancellationToken.None, item.ID, tags);
    }


    /// <summary>
    /// Adds the specified tags to an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> AddTags(CancellationToken cancellationToken, int itemID, string[] tags)
    {
      return await SendTags(cancellationToken, itemID, "tags_add", tags);
    }


    /// <summary>
    /// Adds the specified tags to an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> AddTags(CancellationToken cancellationToken, PocketItem item, string[] tags)
    {
      return await AddTags(cancellationToken, item.ID, tags);
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
      return await RemoveTags(CancellationToken.None, itemID, tags);
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
      return await RemoveTags(CancellationToken.None, item.ID, tags);
    }


    /// <summary>
    /// Removes the specified tags from an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTags(CancellationToken cancellationToken, int itemID, string[] tags)
    {
      return await SendTags(cancellationToken, itemID, "tags_remove", tags);
    }


    /// <summary>
    /// Removes the specified tags from an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <param name="tags">The tag.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTags(CancellationToken cancellationToken, PocketItem item, string[] tags)
    {
      return await RemoveTags(cancellationToken, item.ID, tags);
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
      return await RemoveTag(CancellationToken.None, itemID, tag);
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
      return await RemoveTag(CancellationToken.None, item.ID, tag);
    }


    /// <summary>
    /// Removes a tag from an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tag">The tag.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTag(CancellationToken cancellationToken, int itemID, string tag)
    {
      return await SendTags(cancellationToken, itemID, "tags_remove", new string[] { tag });
    }


    /// <summary>
    /// Removes a tag from an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <param name="tag">The tag.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTag(CancellationToken cancellationToken, PocketItem item, string tag)
    {
      return await RemoveTag(cancellationToken, item.ID, tag);
    }


    /// <summary>
    /// Clears all tags from an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTags(int itemID)
    {
      return await RemoveTags(CancellationToken.None, itemID);
    }


    /// <summary>
    /// Clears all tags from an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTags(PocketItem item)
    {
      return await RemoveTags(CancellationToken.None, item.ID);
    }


    /// <summary>
    /// Clears all tags from an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTags(CancellationToken cancellationToken, int itemID)
    {
      return await SendDefault(cancellationToken, itemID, "tags_clear");
    }


    /// <summary>
    /// Clears all tags from an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTags(CancellationToken cancellationToken, PocketItem item)
    {
      return await RemoveTags(cancellationToken, item.ID);
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
      return await ReplaceTags(CancellationToken.None, itemID, tags);
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
      return await ReplaceTags(CancellationToken.None, item.ID, tags);
    }


    /// <summary>
    /// Replaces all existing tags with the given tags in an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> ReplaceTags(CancellationToken cancellationToken, int itemID, string[] tags)
    {
      return await SendTags(cancellationToken, itemID, "tags_replace", tags);
    }


    /// <summary>
    /// Replaces all existing tags with the given new ones in an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> ReplaceTags(CancellationToken cancellationToken, PocketItem item, string[] tags)
    {
      return await ReplaceTags(cancellationToken, item.ID, tags);
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
      return await RenameTag(CancellationToken.None, itemID, oldTag, newTag);
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
      return await RenameTag(CancellationToken.None, item.ID, oldTag, newTag);
    }


    /// <summary>
    /// Renames a tag in an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <param name="oldTag">The old tag.</param>
    /// <param name="newTag">The new tag name.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RenameTag(CancellationToken cancellationToken, int itemID, string oldTag, string newTag)
    {
      return await Send(new ActionParameter()
      {
        Action = "tag_rename",
        ID = itemID,
        OldTag = oldTag,
        NewTag = newTag
      }, cancellationToken);
    }


    /// <summary>
    /// Renames a tag in an item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <param name="oldTag">The old tag.</param>
    /// <param name="newTag">The new tag name.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RenameTag(CancellationToken cancellationToken, PocketItem item, string oldTag, string newTag)
    {
      return await RenameTag(cancellationToken, item.ID, oldTag, newTag);
    }


    /// <summary>
    /// Puts the send action for tags.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <param name="action">The action.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    protected async Task<bool> SendTags(CancellationToken cancellationToken, int itemID, string action, string[] tags)
    {
      return await Send(new ActionParameter()
      {
        Action = action,
        ID = itemID,
        Tags = tags
      }, cancellationToken);
    }
  }
}
