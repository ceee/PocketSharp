using PocketSharp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PocketSharp.Models.Parameters;

namespace PocketSharp
{
  public partial class PocketClient
  {
    /// <summary>
    /// Retrieves all items from pocket
    /// </summary>
    /// <param name="parameters">parameters, which are mapped to the officials from http://getpocket.com/developer/docs/v3/retrieve </param>
    /// <returns></returns>
    public List<PocketItem> Retrieve(RetrieveParameters parameters)
    {
      return GetResource<Retrieve>("get", parameters.Convert()).Items;
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
          parameters.ContentType = ContentTypeEnum.article;
          break;
        case RetrieveFilter.Image:
          parameters.ContentType = ContentTypeEnum.image;
          break;
        case RetrieveFilter.Video:
          parameters.ContentType = ContentTypeEnum.video;
          break;
        case RetrieveFilter.Favorite:
          parameters.Favorite = true;
          break;
        case RetrieveFilter.Unread:
          parameters.State = StateEnum.unread;
          break;
        case RetrieveFilter.Archive:
          parameters.State = StateEnum.archive;
          break;
      }

      return GetResource<Retrieve>("get",  parameters.Convert()).Items;
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
        Tag = tag
      };
      return GetResource<Retrieve>("get", parameters.Convert()).Items;
    }


    /// <summary>
    /// Retrieves items from pocket which match the specified search string in title or content
    /// </summary>
    /// <param name="tag">The tag.</param>
    /// <returns></returns>
    public List<PocketItem> Search(string searchString)
    {
      RetrieveParameters parameters = new RetrieveParameters()
      {
        Search = searchString
      };
      return GetResource<Retrieve>("get", parameters.Convert()).Items;
    }
  }

  /// <summary>
  /// Filter for simple retrieve requests
  /// </summary>
  public enum RetrieveFilter
  {
    All,
    Unread,
    Archive,
    Favorite,
    Article,
    Video,
    Image
  }
}
