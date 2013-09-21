using PocketSharp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PocketSharp
{
  /// <summary>
  /// PocketClient
  /// </summary>
  public partial class PocketClient
  {
    /// <summary>
    /// Retrieves items from pocket
    /// with the given filters
    /// </summary>
    /// <param name="parameters">parameters, which are mapped to the officials from http://getpocket.com/developer/docs/v3/retrieve </param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<List<PocketItem>> Get(
      State? state = null,
      bool? favorite = null,
      string tag = null,
      ContentType? contentType = null,
      Sort? sort = null,
      string search = null,
      string domain = null,
      DateTime? since = null,
      int? count = null,
      int? offset = null
    )
    {
      RetrieveParameters parameters = new RetrieveParameters()
      {
        State = state,
        Favorite = favorite,
        Tag = tag,
        ContentType = contentType,
        Sort = sort,
        DetailType = DetailType.complete,
        Search = search,
        Domain = domain,
        Since = since,
        Count = count,
        Offset = offset
      };

      Retrieve response = await Request<Retrieve>("get", parameters.Convert());

      return response.Items;
    }


    /// <summary>
    /// Retrieves an item by a given ID
    /// Note: The Pocket API contains no method, which allows to retrieve a single item, so all items are retrieved and filtered locally by the ID.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<PocketItem> Get(int itemID)
    {
      List<PocketItem> items = await Get(
        state: State.all  
      );
      
      return items.SingleOrDefault<PocketItem>(item => item.ID == itemID);
    }


    /// <summary>
    /// Retrieves all items by a given filter
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<List<PocketItem>> Get(RetrieveFilter filter)
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

      Retrieve response = await Request<Retrieve>("get", parameters.Convert());

      return response.Items;
    }


    /// <summary>
    /// Retrieves all available tags.
    /// Note: The Pocket API contains no method, which allows to retrieve all tags, so all items are retrieved and the associated tags extracted.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<List<PocketTag>> GetTags()
    {
      List<PocketItem> items = await Get(
        state: State.all
      );

      return items.Where(item => item.Tags != null)
                  .SelectMany(item => item.Tags)
                  .GroupBy(item => item.Name)
                  .Select(item => item.First())
                  .ToList<PocketTag>();
    }


    /// <summary>
    /// Retrieves items by tag
    /// </summary>
    /// <param name="tag">The tag.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<List<PocketItem>> SearchByTag(string tag)
    {
      return await Get(tag: tag);
    }


    /// <summary>
    /// Retrieves items which match the specified search string in title or content
    /// </summary>
    /// <param name="searchString">The search string.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<List<PocketItem>> Search(string searchString)
    {
      return await Get(search: searchString);
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
