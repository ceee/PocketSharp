using System;
using PocketSharp;
using PocketSharp.Models.Authentification;

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
      PocketClient client = new PocketClient("15396-f6f92101d72c8e270a6c9bb3");

      client.Test();
      
      System.Console.WriteLine(client.Test2());

      System.Console.WriteLine("---------------------------------");

      var result = client.Test3();

      System.Console.WriteLine(string.Format("Code: {0}, Username: {1}", result.Code, result.Username));

      System.Console.ReadKey();
    }
  }
}
