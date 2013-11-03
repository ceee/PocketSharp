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
  }
}