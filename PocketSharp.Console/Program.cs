using System;
using PocketSharp;
using PocketSharp.Models.Authentification;
using PocketSharp.Models.Parameters;
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

      // this apiKey is just for demonstration purposes
      // please create your own application and retrieve it's key. It's a 1-step process ;-)
      PocketClient client = new PocketClient(
        consumerKey: "15396-f6f92101d72c8e270a6c9bb3",
        accessCode: "a85134a7-243c-6656-ab82-97c901"
      );

      List<PocketItem> x = client.Retrieve(RetrieveFilter.Archive);

      foreach(PocketItem item in x)
      {
        System.Console.WriteLine(">>> " + item.Title);  
      }

      System.Console.ReadKey();
    }
  }
}
