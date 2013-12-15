using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PocketSharp.Tests
{
  public class TestsBase : IDisposable
  {
    protected PocketClient client;

    protected List<string> itemsToDelete = new List<string>();


    // setup
    public TestsBase()
    {
      // !! please don't misuse this account !!
      client = new PocketClient(
        consumerKey: "15396-f6f92101d72c8e270a6c9bb3",
        callbackUri: "http://frontendplay.com",
        accessCode: "80acf6c5-c198-03c0-b94c-e74402"
      );
    }


    // teardown
    public void Dispose()
    {
      itemsToDelete.ForEach(async id =>
      {
        await client.Delete(id);
      });
    }


    // async throws
    public static async Task ThrowsAsync<TException>(Func<Task> func)
    {
      var expected = typeof(TException);
      Type actual = null;
      try
      {
        await func();
      }
      catch (Exception e)
      {
        actual = e.GetType();
      }
      Assert.Equal(expected, actual);
    }
  }
}
