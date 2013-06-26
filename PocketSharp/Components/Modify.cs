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
    /// Archives the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    public bool Archive(int itemID)
    {
      return PutSendAction(itemID, "archive");
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
    /// Un-archives the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    public bool Readd(int itemID)
    {
      return Unarchive(itemID);
    }


    /// <summary>
    /// Un-archives the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public bool Readd(PocketItem item)
    {
      return Unarchive(item.ID);
    }


    /// <summary>
    /// Un-archives the specified item (alias for Readd).
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    public bool Unarchive(int itemID)
    {
      return PutSendAction(itemID, "readd");
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
      return PutSendAction(itemID, "favorite");
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
      return PutSendAction(itemID, "unfavorite");
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
      return PutSendAction(itemID, "delete");
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
  }
}
