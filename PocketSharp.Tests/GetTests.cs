using PocketSharp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

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

      PocketItem item = await client.Get("99999999");

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


    [Fact]
    public async Task IsSinceParameterWorkingOnAddTags()
    {
      DateTime since = DateTime.UtcNow;

      List<PocketItem> items = await client.Get(state: State.all);
      PocketItem itemToModify = items[0];

      Assert.True(items.Count >= 3);

      items = await client.Get(state: State.all, since: since);

      Assert.True(items == null || items.Count == 0);

      await client.AddTags(itemToModify, new string[] { "pocketsharp" });

      items = await client.Get(state: State.all, since: since);

      Assert.True(items.Count > 0);

      await client.RemoveTags(itemToModify, new string[] { "pocketsharp" });
    }


    [Fact]
    public async Task IsSinceParameterWorkingOnFavoriteAndArchiveModification()
    {
      DateTime since = DateTime.UtcNow;

      List<PocketItem> items = await client.Get(state: State.all);
      PocketItem itemToModify = items[0];

      Assert.True(items.Count >= 3);

      items = await client.Get(state: State.all, since: since);

      Assert.True(items == null || items.Count == 0);

      await client.Favorite(itemToModify);

      items = await client.Get(state: State.all, since: since);

      Assert.True(items.Count > 0);

      await client.Unfavorite(itemToModify);

      since = DateTime.UtcNow.AddMinutes(-1);

      await client.Archive(itemToModify);

      items = await client.Get(state: State.all, since: since);

      Assert.True(items.Count > 0);

      await client.Unarchive(itemToModify);
    }


    [Fact]
    public async Task IsSinceParameterWorkingOnAddAndDelete()
    {
      DateTime since = DateTime.UtcNow;

      PocketItem item = await client.Add(new Uri("http://frontendplay.com"));

      List<PocketItem> items = await client.Get(state: State.all, since: since);

      since = DateTime.UtcNow;

      await client.Delete(item);

      items = await client.Get(state: State.all, since: since);

      Assert.True(items.Count == 1 && items[0].IsDeleted);
    }

    [Fact]
    public async Task AreUncachedItemsProperlyResolved()
    {
      PocketItem item = await client.Add(new Uri("http://de.ign.com/m/feature/21608/die-20-besten-kurzfilme-des-jahres-2013?bust=1"));

      List<PocketItem> items = await client.Get(state: State.all);

      Assert.NotNull(item.Uri);
      Assert.NotNull(items[0].Uri);
      Assert.Equal(item.Uri, items[0].Uri);

      itemsToDelete.Add(item.ID);
    }
  }
}
