using PocketSharp.Models;

namespace PocketSharp
{
  /// <summary>
  /// PocketClient
  /// </summary>
  public partial class PocketClient
  {
    /// <summary>
    /// Archives the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    public bool Archive(int itemID)
    {
      return PutSendActionDefault(itemID, "archive");
    }


    /// <summary>
    /// Archives the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public bool Archive(PocketItem item)
    {
      return Archive(item.ID);
    }


    /// <summary>
    /// Un-archives the specified item (alias for Readd).
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    public bool Unarchive(int itemID)
    {
      return PutSendActionDefault(itemID, "readd");
    }


    /// <summary>
    /// Unarchives the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public bool Unarchive(PocketItem item)
    {
      return Unarchive(item.ID);
    }


    /// <summary>
    /// Favorites the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    public bool Favorite(int itemID)
    {
      return PutSendActionDefault(itemID, "favorite");
    }


    /// <summary>
    /// Favorites the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public bool Favorite(PocketItem item)
    {
      return Favorite(item.ID);
    }


    /// <summary>
    /// Un-favorites the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    public bool Unfavorite(int itemID)
    {
      return PutSendActionDefault(itemID, "unfavorite");
    }


    /// <summary>
    /// Un-favorites the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public bool Unfavorite(PocketItem item)
    {
      return Unfavorite(item.ID);
    }


    /// <summary>
    /// Deletes the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    public bool Delete(int itemID)
    {
      return PutSendActionDefault(itemID, "delete");
    }


    /// <summary>
    /// Deletes the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public bool Delete(PocketItem item)
    {
      return Delete(item.ID);
    }


    /// <summary>
    /// Puts an action
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    protected bool PutSendActionDefault(int itemID, string action)
    {
      return PutSendAction(new ActionParameter()
      {
        Action = action,
        ID = itemID
      });
    }
  }
}
