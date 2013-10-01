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

      await reader.Read(new Uri("https://github.com/ceee/PocketSharp"));

      Assert.True(true);
    }
  }
}