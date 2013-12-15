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
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> AddTags(string itemID, string[] tags, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await SendTags(cancellationToken, itemID, "tags_add", tags);
    }


    /// <summary>
    /// Adds the specified tags to an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="tags">The tags.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> AddTags(PocketItem item, string[] tags, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await AddTags(item.ID, tags, cancellationToken);
    }


    /// <summary>
    /// Removes the specified tags from an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tags.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTags(string itemID, string[] tags, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await SendTags(cancellationToken, itemID, "tags_remove", tags);
    }


    /// <summary>
    /// Removes the specified tags from an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="tags">The tag.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTags(PocketItem item, string[] tags, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await RemoveTags(item.ID, tags, cancellationToken);
    }


    /// <summary>
    /// Removes a tag from an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tag">The tag.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTag(string itemID, string tag, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await SendTags(cancellationToken, itemID, "tags_remove", new string[] { tag });
    }


    /// <summary>
    /// Removes a tag from an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="tag">The tag.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTag(PocketItem item, string tag, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await RemoveTag(item.ID, tag, cancellationToken);
    }


    /// <summary>
    /// Clears all tags from an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTags(string itemID, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await SendDefault(cancellationToken, itemID, "tags_clear");
    }


    /// <summary>
    /// Clears all tags from an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RemoveTags(PocketItem item, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await RemoveTags(item.ID, cancellationToken);
    }


    /// <summary>
    /// Replaces all existing tags with the given tags in an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="tags">The tags.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> ReplaceTags(string itemID, string[] tags, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await SendTags(cancellationToken, itemID, "tags_replace", tags);
    }


    /// <summary>
    /// Replaces all existing tags with the given new ones in an item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="tags">The tags.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> ReplaceTags(PocketItem item, string[] tags, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await ReplaceTags(item.ID, tags, cancellationToken);
    }


    /// <summary>
    /// Renames a tag in an item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="oldTag">The old tag.</param>
    /// <param name="newTag">The new tag name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RenameTag(string itemID, string oldTag, string newTag, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Send(new PocketAction()
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
    /// <param name="item">The item.</param>
    /// <param name="oldTag">The old tag.</param>
    /// <param name="newTag">The new tag name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> RenameTag(PocketItem item, string oldTag, string newTag, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await RenameTag(item.ID, oldTag, newTag, cancellationToken);
    }


    /// <summary>
    /// Puts the send action for tags.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <param name="action">The action.</param>
    /// <param name="tags">The tags.</param>
    /// <returns></returns>
    protected async Task<bool> SendTags(CancellationToken cancellationToken, string itemID, string action, string[] tags)
    {
      return await Send(new PocketAction()
      {
        Action = action,
        ID = itemID,
        Tags = tags
      }, cancellationToken);
    }
  }
}
