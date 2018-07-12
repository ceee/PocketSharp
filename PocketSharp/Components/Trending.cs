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
    /// Get trending articles on Pocket.
    /// </summary>
    /// <param name="count">Article count.</param>
    /// <param name="languageCode">Two-letter language code for language-specific results.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<IEnumerable<PocketItem>> GetTrendingArticles(int count = 20, string languageCode = "en", CancellationToken cancellationToken = default(CancellationToken))
    {
      return (await Request<Retrieve>("getGlobalRecs", cancellationToken, new Dictionary<string, string>()
      {
        { "locale_lang", languageCode },
        { "count", count.ToString() },
        { "version", "2" }
      }, false)).Items ?? new List<PocketItem>();
    }


    /// <summary>
    /// Get trending topics on Pocket.
    /// </summary>
    /// <param name="languageCode">Two-letter language code for language-specific results.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<IEnumerable<PocketTopic>> GetTrendingTopics(string languageCode = "en", CancellationToken cancellationToken = default(CancellationToken))
    {
      return (await Request<TopicsResponse>("getTrendingTopics", cancellationToken, new Dictionary<string, string>()
      {
        { "locale_lang", languageCode },
        { "version", "2" }
      }, false)).Items ?? new List<PocketTopic>();
    }
  }
}
