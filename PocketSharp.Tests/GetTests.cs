using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using PocketSharp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace PocketSharp.Tests
{
  public class GetTests : TestsBase
  {
    public GetTests() : base() { }


    [Fact]
    public async Task AreItemsRetrieved()
    {
      List<PocketItem> items = (await client.Get()).ToList();
      Assert.True(items.Count > 0);
    }


    [Fact]
    public async Task IsItemRetrievedById()
    {
      List<PocketItem> items = (await client.Get()).ToList();
      PocketItem item = items[0];
      PocketItem itemDuplicate = await client.Get(item.ID);

      Assert.True(item.ID == itemDuplicate.ID);
      Assert.True(item.Uri == itemDuplicate.Uri);
    }

//    [Fact]
//    public async Task IsItemJsonPopulated()
//    {
//      List<PocketItem> items = (await client.Get()).ToList();
//      string schemaJson = @"{
//                'description': 'PocketItem',
//                'type': 'object'
//                }";

//      JsonSchema schema = JsonSchema.Parse(schemaJson);
//      foreach (var pocketItem in items)
//      {
//        Assert.True(!string.IsNullOrWhiteSpace(pocketItem.Json));
//        var jObject = JObject.Parse(pocketItem.Json);
//        Assert.True(jObject.IsValid(schema));
//      }
//    }

    [Fact]
    public async Task AreFilteredItemsRetrieved()
    {
      IEnumerable<PocketItem> items = await client.Get(RetrieveFilter.Favorite);

      Assert.True(items.Count() > 0);
    }


    [Fact]
    public async Task RetrieveWithMultipleFilters()
    {
      IEnumerable<PocketItem> items = await client.Get(
        state: State.unread,
        tag: "pocket",
        sort: Sort.title,
        since: new DateTime(2010, 12, 10),
        count: 2
      );

      Assert.InRange<int>(items.Count(), 0, 2);
    }


    [Fact]
    public async Task ItemContainsUri()
    {
      IEnumerable<PocketItem> items = await client.Get(count: 1);

      Assert.True(items.Count() == 1);
      Assert.True(items.First().Uri.ToString().StartsWith("http"));
    }


    [Fact]
    public async Task InvalidRetrievalReturnsNoResults()
    {
      IEnumerable<PocketItem> items = await client.Get(
        favorite: true,
        search: "xoiu987a#;"
      );

      Assert.False(items.Count() > 0);

      PocketItem item = await client.Get("99999999");

      Assert.Null(item);

      items = await client.Get(RetrieveFilter.Video);

      Assert.False(items.Count() > 0);
    }


    [Fact]
    public async Task SearchReturnsResult()
    {
      IEnumerable<PocketItem> items = await client.Search("pocket");

      Assert.True(items.Count() > 0);
      Assert.True(items.First().FullTitle.ToLower().Contains("pocket"));
    }


    [Fact]
    public async Task InvalidSearchReturnsNoResult()
    {
      IEnumerable<PocketItem> items = await client.Search("adsüasd-opiu2;.398dfyx");

      Assert.False(items.Count() > 0);
    }


    [Fact]
    public async Task RetrieveTagsReturnsResult()
    {
      IEnumerable<PocketTag> items = await client.GetTags();

      Assert.True(items.Count() > 0);
    }


    [Fact]
    public async Task SearchByTagsReturnsResult()
    {
      IEnumerable<PocketItem> items = await client.SearchByTag("pocket");

      Assert.True(items.Count() == 1);

      bool found = false;

      items.First().Tags.ToList().ForEach(tag =>
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
      IEnumerable<PocketItem> items = await client.SearchByTag("adsüasd-opiu2;.398dfyx");

      Assert.False(items.Count() > 0);
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
      IEnumerable<PocketItem> items = await client.Get(count: 1, tag: "pocket");
      PocketItem item = items.First();

      Assert.True(item.Tags.ToList().Find(i => i.Name == "pocket") != null);
    }


    [Fact]
    public async Task IsSinceParameterWorkingOnAddTags()
    {
      DateTime since = DateTime.Now;

      IEnumerable<PocketItem> items = await client.Get(state: State.all);
      PocketItem itemToModify = items.First();

      Assert.True(items.Count() >= 3);

      items = await client.Get(state: State.all, since: since);

      Assert.True(items == null || items.Count() == 0);

      await client.AddTags(itemToModify, new string[] { "pocketsharp" });

      items = await client.Get(state: State.all, since: since);

      Assert.True(items.Count() > 0);

      await client.RemoveTags(itemToModify, new string[] { "pocketsharp" });
    }


    [Fact]
    public async Task IsSinceParameterWorkingOnFavoriteAndArchiveModification()
    {
      DateTime since = DateTime.Now;//1409221736

      IEnumerable<PocketItem> items = await client.Get(state: State.all);
      PocketItem itemToModify = items.First();

      Assert.True(items.Count() >= 3);

      items = await client.Get(state: State.all, since: since);

      Assert.True(items == null || items.Count() == 0);

      await client.Favorite(itemToModify);

      items = await client.Get(state: State.all, since: since);

      Assert.True(items.Count() > 0);

      await client.Unfavorite(itemToModify);

      since = DateTime.Now;

      await client.Archive(itemToModify);

      items = await client.Get(state: State.all, since: since);

      Assert.True(items.Count() > 0);

      await client.Unarchive(itemToModify);
    }


    [Fact]
    public async Task IsUTCSinceParameterWorking()
    {
      DateTime since = DateTime.UtcNow;

      IEnumerable<PocketItem> items = await client.Get(state: State.all);
      PocketItem itemToModify = items.First();

      items = await client.Get(state: State.all, since: since);

      Assert.True(items == null || items.Count() == 0);

      await client.Favorite(itemToModify);

      items = await client.Get(state: State.all, since: since);

      Assert.True(items.Count() > 0);

      await client.Unfavorite(itemToModify);
    }


    [Fact]
    public async Task IsSinceParameterWorkingOnAddAndDelete()
    {
      DateTime since = DateTime.UtcNow;

      PocketItem item = await client.Add(new Uri("http://frontendplay.com"));

      IEnumerable<PocketItem> items = await client.Get(state: State.all, since: since);

      since = DateTime.UtcNow;

      await client.Delete(item);

      items = await client.Get(state: State.all, since: since);

      Assert.True(items.Count() == 1 && items.First().IsDeleted);
    }

    [Fact]
    public async Task AreUncachedItemsProperlyResolved()
    {
      PocketItem item = await client.Add(new Uri("http://de.ign.com/m/feature/21608/die-20-besten-kurzfilme-des-jahres-2013?bust=1"));

      IEnumerable<PocketItem> items = await client.Get(state: State.all);

      Assert.NotNull(item.Uri);
      Assert.NotNull(items.First().Uri);
      Assert.Equal(item.Uri, items.First().Uri);

      itemsToDelete.Add(item.ID);
    }
  }
}
