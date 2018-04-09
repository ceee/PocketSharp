using System;
using System.Threading.Tasks;
using Xunit;

namespace PocketSharp.Tests
{
  public class AccountTests : TestsBase
  {
    public AccountTests() : base() { }


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
