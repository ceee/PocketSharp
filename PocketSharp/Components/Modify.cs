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
    /// Archives the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Archive(int itemID)
    {
      return await Archive(CancellationToken.None, itemID);
    }


    /// <summary>
    /// Archives the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Archive(PocketItem item)
    {
      return await Archive(CancellationToken.None, item.ID);
    }


    /// <summary>
    /// Archives the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Archive(CancellationToken cancellationToken, int itemID)
    {
      return await SendDefault(cancellationToken, itemID, "archive");
    }


    /// <summary>
    /// Archives the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Archive(CancellationToken cancellationToken, PocketItem item)
    {
      return await Archive(cancellationToken, item.ID);
    }


    /// <summary>
    /// Un-archives the specified item (alias for Readd).
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Unarchive(int itemID)
    {
      return await Unarchive(CancellationToken.None, itemID);
    }


    /// <summary>
    /// Unarchives the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Unarchive(PocketItem item)
    {
      return await Unarchive(CancellationToken.None, item.ID);
    }


    /// <summary>
    /// Un-archives the specified item (alias for Readd).
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Unarchive(CancellationToken cancellationToken, int itemID)
    {
      return await SendDefault(cancellationToken, itemID, "readd");
    }


    /// <summary>
    /// Unarchives the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Unarchive(CancellationToken cancellationToken, PocketItem item)
    {
      return await Unarchive(cancellationToken, item.ID);
    }


    /// <summary>
    /// Favorites the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Favorite(int itemID)
    {
      return await Favorite(CancellationToken.None, itemID);
    }


    /// <summary>
    /// Favorites the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Favorite(PocketItem item)
    {
      return await Favorite(CancellationToken.None, item.ID);
    }


    /// <summary>
    /// Favorites the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Favorite(CancellationToken cancellationToken, int itemID)
    {
      return await SendDefault(cancellationToken, itemID, "favorite");
    }


    /// <summary>
    /// Favorites the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Favorite(CancellationToken cancellationToken, PocketItem item)
    {
      return await Favorite(cancellationToken, item.ID);
    }


    /// <summary>
    /// Un-favorites the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Unfavorite(int itemID)
    {
      return await Unfavorite(CancellationToken.None, itemID);
    }


    /// <summary>
    /// Un-favorites the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Unfavorite(PocketItem item)
    {
      return await Unfavorite(CancellationToken.None, item.ID);
    }


    /// <summary>
    /// Un-favorites the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Unfavorite(CancellationToken cancellationToken, int itemID)
    {
      return await SendDefault(cancellationToken, itemID, "unfavorite");
    }


    /// <summary>
    /// Un-favorites the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Unfavorite(CancellationToken cancellationToken, PocketItem item)
    {
      return await Unfavorite(cancellationToken, item.ID);
    }


    /// <summary>
    /// Deletes the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Delete(int itemID)
    {
      return await Delete(CancellationToken.None, itemID);
    }


    /// <summary>
    /// Deletes the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public async Task<bool> Delete(PocketItem item)
    {
      return await Delete(CancellationToken.None, item.ID);
    }


    /// <summary>
    /// Deletes the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Delete(CancellationToken cancellationToken, int itemID)
    {
      return await SendDefault(cancellationToken, itemID, "delete");
    }


    /// <summary>
    /// Deletes the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public async Task<bool> Delete(CancellationToken cancellationToken, PocketItem item)
    {
      return await Delete(cancellationToken, item.ID);
    }


    /// <summary>
    /// Puts an action
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    protected async Task<bool> SendDefault(CancellationToken cancellationToken, int itemID, string action)
    {
      return await Send(new ActionParameter()
      {
        Action = action,
        ID = itemID
      }, cancellationToken);
    }
  }
}
