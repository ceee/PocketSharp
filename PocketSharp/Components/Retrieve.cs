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
    /// Retrieves all items from pocket
    /// </summary>
    /// <param name="parameters">parameters, which are mapped to the officials from http://getpocket.com/developer/docs/v3/retrieve </param>
    /// <returns></returns>
    public List<PocketItem> Retrieve(RetrieveParameters parameters)
    {
      return Get<Retrieve>("get", parameters.Convert(), true).Items;
    }


    /// <summary>
    /// Retrieves all items with a filter from pocket
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    public List<PocketItem> Retrieve(RetrieveFilter filter = RetrieveFilter.All)
    {
      RetrieveParameters parameters = new RetrieveParameters();

      switch(filter)
      {
        case RetrieveFilter.Article:
          parameters.ContentType = ContentType.article;
          break;
        case RetrieveFilter.Image:
          parameters.ContentType = ContentType.image;
          break;
        case RetrieveFilter.Video:
          parameters.ContentType = ContentType.video;
          break;
        case RetrieveFilter.Favorite:
          parameters.Favorite = true;
          break;
        case RetrieveFilter.Unread:
          parameters.State = State.unread;
          break;
        case RetrieveFilter.Archive:
          parameters.State = State.archive;
          break;
      }

      parameters.DetailType = DetailType.complete;

      return Get<Retrieve>("get",  parameters.Convert(), true).Items;
    }


    /// <summary>
    /// Retrieves items by tag from pocket
    /// </summary>
    /// <param name="tag">The tag.</param>
    /// <returns></returns>
    public List<PocketItem> SearchByTag(string tag)
    {
      RetrieveParameters parameters = new RetrieveParameters()
      {
        Tag = tag,
        DetailType = DetailType.complete
      };
      return Get<Retrieve>("get", parameters.Convert(), true).Items;
    }


    /// <summary>
    /// Retrieves items from pocket which match the specified search string in title or content
    /// </summary>
    /// <param name="searchString">The search string.</param>
    /// <returns></returns>
    public List<PocketItem> Search(string searchString)
    {
      RetrieveParameters parameters = new RetrieveParameters()
      {
        Search = searchString,
        DetailType = DetailType.complete
      };
      return Get<Retrieve>("get", parameters.Convert(), true).Items;
    }
  }


  /// <summary>
  /// Filter for simple retrieve requests
  /// </summary>
  public enum RetrieveFilter
  {
    /// <summary>
    /// All types
    /// </summary>
    All,
    /// <summary>
    /// Only unread items
    /// </summary>
    Unread,
    /// <summary>
    /// Archived items
    /// </summary>
    Archive,
    /// <summary>
    /// Favorited items
    /// </summary>
    Favorite,
    /// <summary>
    /// Only articles
    /// </summary>
    Article,
    /// <summary>
    /// Only videos
    /// </summary>
    Video,
    /// <summary>
    /// Only images
    /// </summary>
    Image
  }
}
