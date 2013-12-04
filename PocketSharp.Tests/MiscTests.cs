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

    [Fact]
    public async Task TestNotWorkingUri()
    {
      //PocketItem item = await client.Add(new Uri("http://dl-ghost.azurewebsites.net/ghost-azure/?xy=2"));

      List<PocketItem> items = await client.Get();

      Assert.True(!items[0].IsArchive);
    }
  }
}
