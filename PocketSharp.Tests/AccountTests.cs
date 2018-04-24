using PocketSharp.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PocketSharp.Tests
{
  public class AccountTests : TestsBase
  {
    public AccountTests() : base() { }


    [Fact]
    public async Task IsAuthenticationSuccessful()
    {
      string requestCode = await client.GetRequestCode();

      PocketUser user = await client.GetUser(requestCode);
    }

    [Fact]
    public async Task IsRegistrationURLSuccessfullyCreated()
    {
      string requestCode = await client.GetRequestCode();

      Uri uri = client.GenerateRegistrationUri(requestCode);

      Assert.Contains(requestCode, uri.OriginalString);
      Assert.Contains("force=signup", uri.OriginalString);
    }
  }
}
