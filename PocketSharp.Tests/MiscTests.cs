using PocketSharp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace PocketSharp.Tests
{
  public class MiscTests : TestsBase
  {
    private int Incrementor = 0;

    public MiscTests()
      : base()
    {
      client.PreRequest = method => Incrementor++;
    }


    [Fact]
    public async Task CheckPreRequestAction()
    {
      IEnumerable<PocketItem> items = await client.Get(count: 1);
      PocketItem item = items.First();

      await client.Favorite(item);
      await client.Unfavorite(item);

      Assert.True(Incrementor >= 3);
    }
  }
}
