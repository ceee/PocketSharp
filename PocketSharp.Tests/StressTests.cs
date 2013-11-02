using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using PocketSharp.Models;

namespace PocketSharp.Tests
{
  public class StressTests : TestsBase
  {
    public StressTests() : base() 
    { 
      // !! please don't misuse this account !!
      client = new PocketClient(
        consumerKey: "15396-f6f92101d72c8e270a6c9bb3",
        callbackUri: "http://frontendplay.com",
        accessCode: "80acf6c5-c198-03c0-b94c-e74402"
      );
    }


    [Fact]
    public void Are100ItemsRetrievedProperly()
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