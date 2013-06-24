using PocketSharp.Models;
using System.Collections.Generic;

namespace PocketSharp
{
  public partial class PocketClient
  {
    /// <summary>
    /// Archives the specified item ID.
    /// </summary>
    /// <param name="itemID">The item ID.</param>
    /// <returns></returns>
    public bool Archive(int itemID)
    {
      ActionParameter action = new ActionParameter() 
      { 
        Action = "archive", 
        ID = itemID 
      };

      return Put<Modify>("send", action).Status == 1;
    }
  }
}
