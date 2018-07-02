using PocketSharp.Models;
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
    /// Statistics from the user account.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<IEnumerable<PocketItem>> GetTrendingArticles(string guid, string languageCode = "en", int count = 20, CancellationToken cancellationToken = default(CancellationToken))
    {
      return (await Request<Retrieve>("getGlobalRecs", cancellationToken, new Dictionary<string, string>()
      {
        { "guid", guid },
        { "locale_lang", languageCode },
        { "count", count.ToString() },
        { "version", "2" }
      }, false)).Items ?? new List<PocketItem>();
    }


    /// <summary>
    /// Statistics from the user account.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<IEnumerable<PocketTopic>> GetTrendingTopics(string guid, string languageCode = "en", CancellationToken cancellationToken = default(CancellationToken))
    {
      return (await Request<TopicsResponse>("getTrendingTopics", cancellationToken, new Dictionary<string, string>()
      {
        { "guid", guid },
        { "locale_lang", languageCode },
        { "version", "2" }
      }, false)).Items ?? new List<PocketTopic>();
    }
  }
}
