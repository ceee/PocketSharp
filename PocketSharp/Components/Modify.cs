using PocketSharp.Models;
using System.Collections.Generic;

namespace PocketSharp
{
  public partial class PocketClient
  {
    public bool Archive(int itemID)
    {
      List<ActionParameter> actions = new List<ActionParameter>()
      {
        new ActionParameter() { Action = "archive", ID = itemID }
      };

      return Put<Modify>("send", actions).Status == 1;
    }
  }
}
