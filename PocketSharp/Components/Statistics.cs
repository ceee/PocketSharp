using PocketSharp.Models;
using System;
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
    public async Task<PocketStatistics> GetUserStatistics(CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Request<PocketStatistics>("stats", cancellationToken);
    }


    /// <summary>
    /// Returns API usage statistics.
    /// If a request was made before, the data is returned synchronously from the cache.
    /// Note: This method only works for authenticated users with a given AccessCode.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<PocketLimits> GetUsageLimits(CancellationToken cancellationToken = default(CancellationToken))
    {
      string rateLimitForConsumerKey = TryGetHeaderValue(lastHeaders, "X-Limit-Key-Limit");

      if (rateLimitForConsumerKey == null)
      {
        // this is the fastest way to do a non-failing request to receive the correct headers
        await Get(
          cancellationToken: cancellationToken,
          count: 1
        );
      }

      return new PocketLimits()
      {
        RateLimitForConsumerKey = Convert.ToInt32(TryGetHeaderValue(lastHeaders, "X-Limit-Key-Limit")),
        RemainingCallsForConsumerKey = Convert.ToInt32(TryGetHeaderValue(lastHeaders, "X-Limit-Key-Remaining")),
        SecondsUntilLimitResetsForConsumerKey = Convert.ToInt32(TryGetHeaderValue(lastHeaders, "X-Limit-Key-Reset")),
        RateLimitForUser = Convert.ToInt32(TryGetHeaderValue(lastHeaders, "X-Limit-User-Limit")),
        RemainingCallsForUser = Convert.ToInt32(TryGetHeaderValue(lastHeaders, "X-Limit-User-Remaining")),
        SecondsUntilLimitResetsForUser = Convert.ToInt32(TryGetHeaderValue(lastHeaders, "X-Limit-User-Reset"))
      };
    }
  }
}
