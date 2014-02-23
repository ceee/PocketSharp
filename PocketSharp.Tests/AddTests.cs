using PocketSharp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PocketSharp.Tests
{
  public class AddTests : TestsBase
  {
    public AddTests() : base() { }


    [Fact]
    public async Task AddSimpleItemWithUriOnly()
    {
      var uri = new Uri("http://frontendplay.com");

      PocketItem item = await client.Add(uri);

      Assert.Equal<Uri>(uri, item.Uri);

      itemsToDelete.Add(item.ID);
    }


    [Fact]
    public async Task ItemWithInstableImagesIsAdded()
    {
      var uri = new Uri("http://www.valoronline.com.br");

      PocketItem item = await client.Add(uri);

      Assert.NotNull(item);

      itemsToDelete.Add(item.ID);
    }


    [Fact]
    public async Task AddComplexItem()
    {
      PocketItem item = await client.Add(
        uri: new Uri("http://frontendplay.com"),
        tags: new string[] { "blog", "frontend", "cee" },
        title: "ignored title",
        tweetID: "380051788172632065"
      );

      List<PocketItem> items = await client.Get();
      PocketItem itemDesired = null;

      items.ForEach(itm =>
      {
        if (itm.ID == item.ID)
        {
          itemDesired = itm;
        }
      });

      Assert.NotNull(itemDesired);
      Assert.Equal(itemDesired.ID, item.ID);
      Assert.Equal(itemDesired.Tags.Count, 3);

      itemsToDelete.Add(item.ID);
    }
  }
}
