using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using PocketSharp.Models;

namespace PocketSharp.Tests
{
  public class AccountTests : TestsBase
  {
    public AccountTests() : base() { }


    [Fact]
    public async Task IsUserRegistered()
    {
      string randomId = ((int)((DateTime)DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds).ToString();

      bool success = await client.RegisterAccount("pocket-" + randomId, String.Format("pocketsharp-{0}@outlook.com", randomId), "mypassword");

      Assert.True(success);
    }


    [Fact]
    public async Task AreInvalidRegistrationsBlocked()
    {
      await ThrowsAsync<FormatException>(async () =>
      {
        await client.RegisterAccount("abcdefghijklmnopqrstuvwxyz", "pocketsharp@outlook.com", "mypassword");
      });

      await ThrowsAsync<FormatException>(async () =>
      {
        await client.RegisterAccount("oiu_my:o;", "pocketsharp@outlook.com", "mypassword");
      });

      await ThrowsAsync<FormatException>(async () =>
      {
        await client.RegisterAccount("myusername", "pocketsharpoutlook.com", "mypassword");
      });

      await ThrowsAsync<FormatException>(async () =>
      {
        await client.RegisterAccount("myusername", "pocketsharp@outlook", "mypassword");
      });

      await ThrowsAsync<FormatException>(async () =>
      {
        await client.RegisterAccount("myusername", "pocketsharp@outlook,com", "mypassword");
      });

      await ThrowsAsync<ArgumentNullException>(async () =>
      {
        await client.RegisterAccount("myusername", null, "mypassword");
      });
    }
  }
}
