using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using PocketSharp.Models;

namespace PocketSharp.Tests
{
  public class ReadTests : TestsBase
  {
    public ReadTests() : base() { }


    [Fact]
    public async Task TemporaryReadTest()
    {
      PocketReader reader = new PocketReader();

      string result = await reader.Read(new Uri("http://frontendplay.com/story/4/http-caching-demystified-part-2-implementation"));

      Assert.True(result.Length > 15000);
    }
  }
}