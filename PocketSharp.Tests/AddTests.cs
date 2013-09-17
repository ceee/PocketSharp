using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using PocketSharp.Models;

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
    public async Task AddComplexItem()
    {
      PocketItem item = await client.Add(
        uri: new Uri("http://frontendplay.com"),
        tags: new string[] { "blog", "frontend", "cee" },
        title: "ignored title",
        tweetID: "380051788172632065"
      );

      List<PocketItem> items = await client.Retrieve();
      PocketItem itemDesired = null;

      items.ForEach(itm =>
      {
        if(itm.ID == item.ID)
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
