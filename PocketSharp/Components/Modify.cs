using PocketSharp.Models;
using System;
using System.Collections.Generic;
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
    /// Sends multiple actions in one request.
    /// See: http://getpocket.com/developer/docs/v3/modify
    /// </summary>
    /// <param name="actions">The actions.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> SendActions(IEnumerable<PocketAction> actions, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Send(actions, cancellationToken);
    }


    /// <summary>
    /// Sends an action.
    /// See: http://getpocket.com/developer/docs/v3/modify
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> SendAction(PocketAction action, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Send(new List<PocketAction>() { action }, cancellationToken);
    }


    /// <summary>
    /// Archives the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Archive(string itemID, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await SendDefault(cancellationToken, itemID, "archive");
    }


    /// <summary>
    /// Archives the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Archive(PocketItem item, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Archive(item.ID, cancellationToken);
    }


    /// <summary>
    /// Un-archives the specified item (alias for Readd).
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Unarchive(string itemID, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await SendDefault(cancellationToken, itemID, "readd");
    }


    /// <summary>
    /// Unarchives the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Unarchive(PocketItem item, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Unarchive(item.ID, cancellationToken);
    }


    /// <summary>
    /// Favorites the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Favorite(string itemID, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await SendDefault(cancellationToken, itemID, "favorite");
    }


    /// <summary>
    /// Favorites the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Favorite(PocketItem item, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Favorite(item.ID, cancellationToken);
    }


    /// <summary>
    /// Un-favorites the specified item.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Unfavorite(string itemID, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await SendDefault(cancellationToken, itemID, "unfavorite");
    }


    /// <summary>
    /// Un-favorites the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Unfavorite(PocketItem item, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Unfavorite(item.ID, cancellationToken);
    }


    /// <summary>
    /// Deletes the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<bool> Delete(string itemID, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await SendDefault(cancellationToken, itemID, "delete");
    }


    /// <summary>
    /// Deletes the specified item.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public async Task<bool> Delete(PocketItem item, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Delete(item.ID, cancellationToken);
    }


    /// <summary>
    /// Puts an action
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    protected async Task<bool> SendDefault(CancellationToken cancellationToken, string itemID, string action)
    {
      return await Send(new PocketAction()
      {
        Action = action,
        ID = itemID
      }, cancellationToken);
    }
  }
}
