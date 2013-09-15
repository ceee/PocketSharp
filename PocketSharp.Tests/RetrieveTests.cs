using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using PocketSharp;
using PocketSharp.Models;
using System.Diagnostics;

namespace PocketSharp.Tests
{

  public class RetrieveTests : IDisposable
  {
    private PocketClient client;


    // setup
    public RetrieveTests()
    {
      // !! please don't misuse this account !!
      client = new PocketClient(
        consumerKey: "15396-f6f92101d72c8e270a6c9bb3",
        callbackUri: "http://frontendplay.com",
        accessCode: "2c62cd50-b78a-5558-918b-65adae"
      );
    }


    [Fact]
    public async Task AreItemsRetrieved()
    {
      List<PocketItem> items = await client.Retrieve();

      Assert.True(items.Count > 0);
    }


    [Fact]
    public async Task ItemContainsUri()
    {
      List<PocketItem> items = await client.Retrieve(new RetrieveParameters()
      {
        Count = 1  
      });

      Assert.True(items.Count == 1);

      PocketItem item = items[0];

      Assert.True(item.Uri.ToString().StartsWith("http"));
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
