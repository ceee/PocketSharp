using PocketSharp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PocketSharp.Tests
{
  public class TrendingTests : TestsBase
  {
    [Fact]
    public async Task AreTrendingArticlesReturned()
    {
      string guid = await client.GetGuid();
      var articles = await client.GetTrendingArticles(guid);

      Assert.NotEmpty(articles);
    }


    [Fact]
    public async Task AreTrendingTopicsReturned()
    {
      string guid = await client.GetGuid();
      var topics = await client.GetTrendingTopics(guid);

      Assert.NotEmpty(topics);
    }
  }
}
