using PocketSharp.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PocketSharp.Tests
{
  public class ReadTests : TestsBase
  {
    private PocketReader reader;


    public ReadTests()
      : base()
    {
      reader = new PocketReader();
    }


    [Fact]
    public async Task ReadArticleTest()
    {
      PocketArticle result = await reader.Read(new PocketItem()
      {
        ID = 99,
        Uri = new Uri("http://frontendplay.com/story/4/http-caching-demystified-part-2-implementation")
      });

      Assert.DoesNotContain("<!DOCTYPE html>", result.Content);
      Assert.Contains("<h1>", result.Content);
      Assert.True(result.Content.Length > 15000);
    }


    [Fact]
    public async Task ReadArticleWithContainerNoHeadlineTest()
    {
      PocketArticle result = await reader.Read(new PocketItem()
      {
        ID = 99,
        Uri = new Uri("http://frontendplay.com/story/4/http-caching-demystified-part-2-implementation")
      }, false, true);

      Assert.Contains("<!DOCTYPE html>", result.Content);
      Assert.DoesNotContain("<h1>", result.Content);
      Assert.True(result.Content.Length > 15000);
    }


    [Fact]
    public async Task ReadArticleWithInvalidUriTest()
    {
      await ThrowsAsync<PocketException>(async () =>
      {
        await reader.Read(new PocketItem()
        {
          ID = 99,
          Uri = new Uri("http://frontendplayyyyy.com")
        });
      });
    }


    [Fact]
    public async Task IsBodyOnlyProperlyResolved()
    {
      PocketArticle result = await reader.Read(new PocketItem()
      {
        ID = 99,
        Uri = new Uri("http://calebjacob.com/tooltipster/")
      });

      Assert.True(result.Content.Substring(0, 4) == "<div");
    }
  }
}