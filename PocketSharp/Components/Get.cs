using PocketSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <param name="cancellationToken">The cancellation token.</param>
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
      int? offset = null,
      CancellationToken cancellationToken = default(CancellationToken)
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
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<PocketItem> Get(string itemID, CancellationToken cancellationToken = default(CancellationToken))
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
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<List<PocketItem>> Get(RetrieveFilter filter, CancellationToken cancellationToken = default(CancellationToken))
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
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<List<PocketTag>> GetTags(CancellationToken cancellationToken = default(CancellationToken))
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
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<List<PocketItem>> SearchByTag(string tag, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Get(
        state: State.all,
        cancellationToken: cancellationToken,
        tag: tag
      );
    }


    /// <summary>
    /// Retrieves items which match the specified search string in title and URI
    /// </summary>
    /// <param name="searchString">The search string.</param>
    /// <param name="tag">Filter by tag.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Search string length has to be a minimum of 2 chars</exception>
    /// <exception cref="PocketException"></exception>
    public async Task<List<PocketItem>> Search(string searchString, string tag = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      if (String.IsNullOrEmpty(searchString) || searchString.Length < 2)
      {
        throw new ArgumentOutOfRangeException("Search string length has to be a minimum of 2 chars");
      }

      return await Get(
        state: State.all, 
        search: searchString, 
        tag: tag, 
        cancellationToken: cancellationToken
      );
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
