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

      //Uri redirect = client.Authenticate(new Uri("http://example.com"));

      //System.Console.WriteLine(redirect.ToString());

      //System.Console.WriteLine("---------------------------------");
      //System.Console.WriteLine("Press Any key after you've authenticated the user via the given URI");

      //System.Console.ReadKey();

      //System.Console.WriteLine("---------------------------------");

      //System.Console.WriteLine(client.GetAccessCode());

      client.Search("css").ForEach(delegate(PocketItem item)
        {
          System.Console.WriteLine(item.ID + " ::: " + item.FullTitle);
        });
      
      System.Console.WriteLine("---------------------------------");

      client.Archive(330361896);

      
      System.Console.ReadKey();
    }
  }
}
