using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using PocketSharp.Models;

namespace PocketSharp.Tests
{
  public class GetTests : TestsBase
  {
    public GetTests() : base() { }


    [Fact]
    public async Task AreItemsRetrieved()
    {
      List<PocketItem> items = await client.Get();

      Assert.True(items.Count > 0);
    }


    [Fact]
    public async Task IsItemRetrievedById()
    {
      List<PocketItem> items = await client.Get();
      PocketItem item = items[0];
      PocketItem itemDuplicate = await client.Get(item.ID);

      Assert.True(item.ID == itemDuplicate.ID);
      Assert.True(item.Uri == itemDuplicate.Uri);
    }


    [Fact]
    public async Task AreFilteredItemsRetrieved()
    {
      List<PocketItem> items = await client.Get(RetrieveFilter.Favorite);

      Assert.True(items.Count > 0);
    }


    [Fact]
    public async Task RetrieveWithMultipleFilters()
    {
      List<PocketItem> items = await client.Get(
        state: State.unread,
        tag: "pocket",
        sort: Sort.title,
        since: new DateTime(2010, 12, 10),
        count: 2
      );

      Assert.InRange<int>(items.Count, 0, 2);
    }


    [Fact]
    public async Task ItemContainsUri()
    {
      List<PocketItem> items = await client.Get(count: 1);

      Assert.True(items.Count == 1);
      Assert.True(items[0].Uri.ToString().StartsWith("http"));
    }


    [Fact]
    public async Task InvalidRetrievalReturnsNoResults()
    {
      List<PocketItem> items = await client.Get(
        favorite: true,
        search: "xoiu987a#;"
      );

      Assert.False(items.Count > 0);

      PocketItem item = await client.Get(99999999);

      Assert.Null(item);

      items = await client.Get(RetrieveFilter.Video);

      Assert.False(items.Count > 0);
    }


    [Fact]
    public async Task SearchReturnsResult()
    {
      List<PocketItem> items = await client.Search("pocket");

      Assert.True(items.Count > 0);
      Assert.True(items[0].FullTitle.ToLower().Contains("pocket"));
    }


    [Fact]
    public async Task InvalidSearchReturnsNoResult()
    {
      List<PocketItem> items = await client.Search("adsüasd-opiu2;.398dfyx");

      Assert.False(items.Count > 0);
    }


    [Fact]
    public async Task RetrieveTagsReturnsResult()
    {
      List<PocketTag> items = await client.GetTags();

      Assert.True(items.Count > 0);
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


    [Fact]
    public async Task InvalidSearchByTagsReturnsNoResult()
    {
      List<PocketItem> items = await client.SearchByTag("adsüasd-opiu2;.398dfyx");

      Assert.False(items.Count > 0);
    }


    [Fact]
    public async Task AreStatisticsRetrieved()
    {
      PocketStatistics statistics = await client.GetUserStatistics();

      Assert.True(statistics.CountAll > 0);
    }


    [Fact]
    public async Task AreLimitsRetrieved()
    {
      PocketLimits limits = await client.GetUsageLimits();

      Assert.True(limits.RateLimitForConsumerKey > 9999);
    }


    [Fact]
    public async Task IsNewPocketItemListGeneratorWorking()
    {
      List<PocketItem> items = await client.Get(count: 1, tag: "pocket");
      PocketItem item = items[0];

      Assert.True(item.Tags.Find(i => i.Name == "pocket") != null);
    }
  }
}
