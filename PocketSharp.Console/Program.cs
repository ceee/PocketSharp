using System;
using PocketSharp;
using PocketSharp.Models.Authentification;
using PocketSharp.Models;
using System.Collections.Generic;

namespace PocketSharp.Console
{
  class Program
  {
    static void Main(string[] args)
    {
      System.Console.WriteLine("PocketClient internal usage tests");
      System.Console.WriteLine("---------------------------------");

      // this consumerKey is just for demonstration purposes
      // please create your own application and retrieve it's key. It's a 1-step process ;-)
      PocketClient client = new PocketClient(
        consumerKey: "15396-f6f92101d72c8e270a6c9bb3"
      );

      var x = client.GetAuthentificationUri(new Uri("http://ceecore.com"));

      System.Console.WriteLine(x.ToString());
      
      System.Console.ReadKey();
    }
  }
}
