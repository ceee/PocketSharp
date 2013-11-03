using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using PocketSharp.Models;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace PocketSharp.Tests
{
  public class StressTests : TestsBase
  {
    private static IEnumerable<string> urls;
    private static string[] tags = new string[]{ "css", "js", "csharp", "windows", "microsoft" };


    public StressTests() : base() 
    { 
      // !! please don't misuse this account !!
      client = new PocketClient(
        consumerKey: "20000-786d0bc8c39294e9829111d6",
        callbackUri: "http://frontendplay.com",
        accessCode: "9b8ecb6b-7801-1a5c-7b39-2ba05b"
      );

      urls = File.ReadAllLines("../../url-10000.csv").Select(item => item.Split(',')[1]);

      //await FillAccount(598, 10000);
    }


    [Fact]
    public async Task Are100ItemsRetrievedProperly()
    {
      
    }

    [Fact]
    public void Are1000ItemsRetrievedProperly()
    {

    }

    [Fact]
    public void Are10000ItemsRetrievedProperly()
    {

    }

    [Fact]
    public void Are0ItemsRetrievedProperly()
    {
      
    }

    [Fact]
    public void IsSearchedSuccessfullyOn10000Items()
    {
      
    }


    private async Task FillAccount(int offset, int count)
    {
      int r;
      int r2;
      string[] tag;
      Random rnd = new Random();

      foreach (string url in urls.Skip(offset).Take(count))
      {
        r = rnd.Next(tags.Length);
        r2 = rnd.Next(tags.Length);
        tag = new string[] { tags[r], tags[r2] };
        await client.Add(new Uri("http://" + url), tag);
      }
    }
  }
}