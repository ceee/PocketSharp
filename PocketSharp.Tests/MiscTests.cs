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

    [Fact]
    public void ItemEqualityChecks()
    {
      PocketItem item1 = new PocketItem() { ID = "12872" };
      PocketItem item2 = new PocketItem() { ID = "12872" };
      PocketItem item3 = new PocketItem() { ID = "12800" };
      PocketArticle article = new PocketArticle();

      Assert.True(item1.Equals(item2));
      Assert.False(item1.Equals(item3));
      Assert.False(item1.Equals(null));

      Assert.True(item1 == item2);
      Assert.False(item1 == item3);
      Assert.False(item1 == null);

      Assert.False(item1 != item2);
      Assert.True(item1 != item3);
      Assert.True(item1 != null);

      Assert.False(item1.Equals(article));

      Assert.True(new List<PocketItem>() { item1 }.IndexOf(item2) > -1);
    }
  }
}
