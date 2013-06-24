using PocketSharp.Models;
using System.Collections.Generic;

namespace PocketSharp
{
  public partial class PocketClient
  {
    /// <summary>
    /// Archives the specified item ID.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    public bool Archive(int itemID)
    {
      return PutAction(itemID, "archive");
    }


    /// <summary>
    /// Un-archives the specified item ID.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    public bool Unarchive(int itemID)
    {
      return PutAction(itemID, "readd");
    }


    /// <summary>
    /// Favorites the specified item ID.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    public bool Favorite(int itemID)
    {
      return PutAction(itemID, "favorite");
    }


    /// <summary>
    /// Un-favorites the specified item ID.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    public bool Unfavorite(int itemID)
    {
      return PutAction(itemID, "unfavorite");
    }


    /// <summary>
    /// Deletes the specified item ID.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    public bool Delete(int itemID)
    {
      return PutAction(itemID, "delete");
    }
  }
}
