using RestSharp;
using ServiceStack.Text;
using System;
using System.Collections.Generic;

namespace PocketSharp.Models
{
  public class ModifyParameters
  {
    public List<ActionParameter> Actions { get; set; }


    public List<Parameter> Convert()
    {
      List<object> actions = new List<object>();

      Actions.ForEach(action => actions.Add(action.Convert()));

      return new List<Parameter>()
      {
        Utilities.CreateParam("actions", JsonSerializer.SerializeToString(actions))
      };
    }
  }
}
