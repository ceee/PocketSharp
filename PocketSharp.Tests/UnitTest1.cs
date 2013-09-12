using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace PocketSharp.Tests
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public async Task TestMethod1()
    {
      var tcs = new TaskCompletionSource<string>();

      PocketClient client = new PocketClient(
        consumerKey: "15396-f6f92101d72c8e270a6c9bb3",
        callbackUri: "http://frontendplay.com",
        accessCode: "206d5286-230d-b3e2-48c6-fabf89"
      );

      var items = await client.Retrieve(RetrieveFilter.Archive);

      Assert.AreEqual(10, items.Capacity);
    }
  }
}
