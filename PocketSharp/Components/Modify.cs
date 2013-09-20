using PocketSharp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Archive(int itemID)
    {
      return await SendDefault(itemID, "archive");
    }


    /// <summary>
    /// Archives the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Archive(PocketItem item)
    {
      return await Archive(item.ID);
    }


    /// <summary>
    /// Un-archives the specified item (alias for Readd).
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Unarchive(int itemID)
    {
      return await SendDefault(itemID, "readd");
    }


    /// <summary>
    /// Unarchives the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Unarchive(PocketItem item)
    {
      return await Unarchive(item.ID);
    }


    /// <summary>
    /// Favorites the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Favorite(int itemID)
    {
      return await SendDefault(itemID, "favorite");
    }


    /// <summary>
    /// Favorites the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Favorite(PocketItem item)
    {
      return await Favorite(item.ID);
    }


    /// <summary>
    /// Un-favorites the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Unfavorite(int itemID)
    {
      return await SendDefault(itemID, "unfavorite");
    }


    /// <summary>
    /// Un-favorites the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Unfavorite(PocketItem item)
    {
      return await Unfavorite(item.ID);
    }


    /// <summary>
    /// Deletes the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Delete(int itemID)
    {
      return await SendDefault(itemID, "delete");
    }


    /// <summary>
    /// Deletes the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public async Task<bool> Delete(PocketItem item)
    {
      return await Delete(item.ID);
    }


    /// <summary>
    /// Puts an action
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    protected async Task<bool> SendDefault(int itemID, string action)
    {
      return await Send(new ActionParameter()
      {
        Action = action,
        ID = itemID
      });
    }
  }
}
