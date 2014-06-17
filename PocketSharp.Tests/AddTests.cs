using PocketSharp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

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

      List<PocketItem> items = (await client.Get()).ToList();
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
      Assert.Equal(itemDesired.Tags.Count(), 3);

      itemsToDelete.Add(item.ID);
    }


    [Fact]
    public async Task ItemViaActionsIsAdded()
    {
      List<PocketAction> actions = new List<PocketAction>();

      actions.Add(new PocketAction()
      {
        Uri = new Uri("http://frontendplay.com/story/4015/string-indexer-for-text-resources-in-nancy"),
        Action = "add",
        Time = DateTime.Now
      });

      bool success = await client.SendActions(actions);

      Assert.True(success);

      IEnumerable<PocketItem> items = await client.Search("in nancy");

      Assert.NotNull(items);
      Assert.True(items.Count() > 0);

      itemsToDelete.Add(items.First().ID);
    }


    [Fact]
    public async Task ItemsViaActionsIsAdded()
    {
      List<PocketAction> actions = new List<PocketAction>();

      actions.Add(new PocketAction()
      {
        Uri = new Uri("http://frontendplay.com/story/4015/string-indexer-for-text-resources-in-nancy"),
        Action = "add",
        Time = DateTime.Now
      });
      actions.Add(new PocketAction()
      {
        Uri = new Uri("http://frontendplay.com/story/4013/build-a-custom-razor-viewbase-in-nancy"),
        Action = "add",
        Time = DateTime.Now
      });

      bool success = await client.SendActions(actions);

      Assert.True(success);

      IEnumerable<PocketItem> items = await client.Search("in nancy");

      Assert.NotNull(items);
      Assert.True(items.Count() == 2);

      itemsToDelete.AddRange(items.Select(item => item.ID));
    }
  }
}
