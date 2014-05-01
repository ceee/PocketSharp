using PocketSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PocketSharp.Tests
{
  public class ModifyTagsTests : TestsBase
  {
    public ModifyTagsTests() : base() { }


    [Fact]
    public async Task AreTagsAddedAndDeletedToAnItem()
    {
      PocketItem item = await Setup();

      Assert.True(await client.AddTags(item, new string[] { "test_tag", "test_tag2" }));

      item = await GetItemById(item.ID);

      Assert.True(item.Tags.Count() >= 2);

      Assert.NotNull(item.Tags.Single<PocketTag>(tag => tag.Name == "test_tag"));
      Assert.NotNull(item.Tags.Single<PocketTag>(tag => tag.Name == "test_tag2"));

      Assert.True(await client.RemoveTags(item, new string[] { "test_tag", "test_tag2" }));

      item = await GetItemById(item.ID);

      Assert.Null(item.Tags.SingleOrDefault<PocketTag>(tag => tag.Name == "test_tag"));
      Assert.Null(item.Tags.SingleOrDefault<PocketTag>(tag => tag.Name == "test_tag2"));
    }


    [Fact]
    public async Task AreAllTagsRemovedFromItem()
    {
      PocketItem item = await Setup();

      Assert.True(await client.AddTags(item, new string[] { "test_tag", "test_tag2" }));

      item = await GetItemById(item.ID);

      Assert.True(item.Tags.Count() >= 2);

      Assert.True(await client.RemoveTags(item));

      item = await GetItemById(item.ID);

      Assert.Null(item.Tags);
    }


    [Fact]
    public async Task AreTagsReplaced()
    {
      PocketItem item = await Setup();

      Assert.True(await client.ReplaceTags(item.ID, new string[] { "test_tag", "test_tag2" }));

      item = await GetItemById(item.ID);

      Assert.Equal(item.Tags.Count(), 2);

      Assert.NotNull(item.Tags.SingleOrDefault<PocketTag>(tag => tag.Name == "test_tag"));
      Assert.NotNull(item.Tags.SingleOrDefault<PocketTag>(tag => tag.Name == "test_tag2"));
    }


    private async Task<PocketItem> Setup()
    {
      PocketItem item = await client.Add(
        uri: new Uri("https://github.com"),
        tags: new string[] { "github", "code", "social" }
      );

      itemsToDelete.Add(item.ID);

      return await GetItemById(item.ID);
    }


    private async Task<PocketItem> GetItemById(string id, bool archive = false)
    {
      List<PocketItem> items = (await client.Get(state: archive ? State.archive : State.unread)).ToList();
      PocketItem itemDesired = null;

      items.ForEach(itm =>
      {
        if (itm.ID == id)
        {
          itemDesired = itm;
        }
      });

      return itemDesired;
    }
  }
}
