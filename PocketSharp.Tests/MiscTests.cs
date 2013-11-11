using PocketSharp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

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
      List<PocketItem> items = await client.Get(count: 1);
      PocketItem item = items[0];

      await client.Favorite(item);
      await client.Unfavorite(item);

      Assert.True(Incrementor >= 3);
    }
  }
}
