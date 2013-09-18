using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using PocketSharp.Models;

namespace PocketSharp.Tests
{
  public class RetrieveTests : TestsBase
  {
    public RetrieveTests() : base() { }


    [Fact]
    public async Task AreItemsRetrieved()
    {
      List<PocketItem> items = await client.Retrieve();

      Assert.True(items.Count > 0);
    }


    [Fact]
    public async Task AreFilteredItemsRetrieved()
    {
      List<PocketItem> items = await client.RetrieveByFilter(RetrieveFilter.Favorite);

      Assert.True(items.Count > 0);
    }


    [Fact]
    public async Task RetrieveWithMultipleFilters()
    {
      List<PocketItem> items = await client.Retrieve(
        state: State.unread,
        tag: "pocket",
        contentType: ContentType.article,
        since: new DateTime(2010, 12, 10),
        count: 2
      );

      Assert.True(items.Count > 0);
    }


    [Fact]
    public async Task ItemContainsUri()
    {
      List<PocketItem> items = await client.Retrieve(count: 1);

      Assert.True(items.Count == 1);
      Assert.True(items[0].Uri.ToString().StartsWith("http"));
    }


    [Fact]
    public async Task SearchReturnsResult()
    {
      List<PocketItem> items = await client.Search("pocket");

      Assert.True(items.Count > 0);
      Assert.True(items[0].FullTitle.ToLower().Contains("pocket"));
    }


    [Fact]
    public async Task SearchByTagsReturnsResult()
    {
      List<PocketItem> items = await client.SearchByTag("pocket");

      Assert.True(items.Count == 1);

      bool found = false;

      items[0].Tags.ForEach(tag =>
      {
        if (tag.Name.Contains("pocket"))
        {
          found = true;
        }
      });

      Assert.True(found);
    }
  }
}
