using PocketSharp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

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
    /// <param name="state">The state.</param>
    /// <param name="favorite">The favorite.</param>
    /// <param name="tag">The tag.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="sort">The sort.</param>
    /// <param name="search">The search.</param>
    /// <param name="domain">The domain.</param>
    /// <param name="since">The since.</param>
    /// <param name="count">The count.</param>
    /// <param name="offset">The offset.</param>
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
      return await Get(CancellationToken.None, state, favorite, tag, contentType, sort, search, domain, since, count, offset);
    }


    /// <summary>
    /// Retrieves items from pocket
    /// with the given filters
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="state">The state.</param>
    /// <param name="favorite">The favorite.</param>
    /// <param name="tag">The tag.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="sort">The sort.</param>
    /// <param name="search">The search.</param>
    /// <param name="domain">The domain.</param>
    /// <param name="since">The since.</param>
    /// <param name="count">The count.</param>
    /// <param name="offset">The offset.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<List<PocketItem>> Get(
      CancellationToken cancellationToken,
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

      Retrieve response = await Request<Retrieve>("get", cancellationToken, parameters.Convert());

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
      return await Get(CancellationToken.None, itemID);
    }


    /// <summary>
    /// Retrieves an item by a given ID
    /// Note: The Pocket API contains no method, which allows to retrieve a single item, so all items are retrieved and filtered locally by the ID.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<PocketItem> Get(CancellationToken cancellationToken, int itemID)
    {
      List<PocketItem> items = await Get(
        cancellationToken: cancellationToken,
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
      return await Get(CancellationToken.None, filter);
    }


    /// <summary>
    /// Retrieves all items by a given filter
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<List<PocketItem>> Get(CancellationToken cancellationToken, RetrieveFilter filter)
    {
      RetrieveParameters parameters = new RetrieveParameters();

      switch (filter)
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
        case RetrieveFilter.All:
          parameters.State = State.all;
          break;
      }

      parameters.DetailType = DetailType.complete;

      Retrieve response = await Request<Retrieve>("get", cancellationToken, parameters.Convert());

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
      return await GetTags(CancellationToken.None);
    }


    /// <summary>
    /// Retrieves all available tags.
    /// Note: The Pocket API contains no method, which allows to retrieve all tags, so all items are retrieved and the associated tags extracted.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<List<PocketTag>> GetTags(CancellationToken cancellationToken)
    {
      List<PocketItem> items = await Get(
        cancellationToken: cancellationToken,
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
      return await SearchByTag(CancellationToken.None, tag);
    }


    /// <summary>
    /// Retrieves items by tag
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="tag">The tag.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<List<PocketItem>> SearchByTag(CancellationToken cancellationToken, string tag)
    {
      return await Get(
        cancellationToken: cancellationToken,
        tag: tag
      );
    }


    /// <summary>
    /// Retrieves items which match the specified search string in title and URI
    /// </summary>
    /// <param name="searchString">The search string.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Search string length has to be a minimum of 2 chars</exception>
    /// <exception cref="PocketException"></exception>
    public async Task<List<PocketItem>> Search(string searchString, bool searchInUri = true)
    {
      return await Search(CancellationToken.None, searchString, searchInUri);
    }


    /// <summary>
    /// Retrieves items which match the specified search string in title and URI
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="searchString">The search string.</param>
    /// <param name="searchInUri">if set to <c>true</c> [search in URI].</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Search string length has to be a minimum of 2 chars</exception>
    /// <exception cref="PocketException"></exception>
    public async Task<List<PocketItem>> Search(CancellationToken cancellationToken, string searchString, bool searchInUri = true)
    {
      List<PocketItem> items = await Get(cancellationToken, RetrieveFilter.All);
      return Search(items, searchString);
    }


    /// <summary>
    /// Finds the specified search string in title and URI for an available list of items
    /// </summary>
    /// <param name="availableItems">The available items.</param>
    /// <param name="searchString">The search string.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Search string length has to be a minimum of 2 chars</exception>
    /// <exception cref="PocketException"></exception>
    public List<PocketItem> Search(List<PocketItem> availableItems, string searchString)
    {
      if (searchString.Length < 2)
      {
        throw new ArgumentOutOfRangeException("Search string length has to be a minimum of 2 chars");
      }

      return availableItems.Where(item => (
        (!String.IsNullOrEmpty(item.FullTitle) && item.FullTitle.ToLower().Contains(searchString))
        || item.Uri.ToString().ToLower().Contains(searchString)
      )).ToList();
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
