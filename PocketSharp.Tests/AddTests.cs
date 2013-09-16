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
  }
}
