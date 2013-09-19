using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using PocketSharp.Models;

namespace PocketSharp.Tests
{
  public class ModifyTests : TestsBase
  {
    public ModifyTests() : base() { }


    [Fact]
    public async Task IsAnItemArchivedAndUnarchived()
    {
      PocketItem item = await Setup();

      Assert.True(await client.Archive(item));

      item = await GetItemById(item.ID, true);

      Assert.True(item.IsArchive);

      Assert.True(await client.Unarchive(item));

      item = await GetItemById(item.ID);

      Assert.False(item.IsArchive);
    }


    [Fact]
    public async Task IsAnItemFavoritedAndUnfavorited()
    {
      PocketItem item = await Setup();

      Assert.True(await client.Favorite(item));

      item = await GetItemById(item.ID);

      Assert.True(item.IsFavorite);

      Assert.True(await client.Unfavorite(item));

      item = await GetItemById(item.ID);

      Assert.False(item.IsFavorite);
    }


    [Fact]
    public async Task IsAnItemDeleted()
    {
      PocketItem item = await Setup();

      Assert.True(await client.Delete(item));

      item = await GetItemById(item.ID);

      Assert.Null(item);
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


    private async Task<PocketItem> GetItemById(int id, bool archive = false)
    {
      List<PocketItem> items = await client.Retrieve(state: archive ? State.archive : State.unread);
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
