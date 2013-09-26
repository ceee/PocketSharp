using PocketSharp.Models;
using System;
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
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
    public async Task<PocketStatistics> Statistics()
    {
      return await Request<PocketStatistics>("stats");
    }
  }
}
