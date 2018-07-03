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
      var articles = await client.GetTrendingArticles();
      Assert.NotEmpty(articles);
    }


    [Fact]
    public async Task AreTrendingTopicsReturned()
    {
      var topics = await client.GetTrendingTopics();
      Assert.NotEmpty(topics);
    }
  }
}
