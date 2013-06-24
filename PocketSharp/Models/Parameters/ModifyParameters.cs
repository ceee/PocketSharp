using RestSharp;
using ServiceStack.Text;
using System;
using System.Collections.Generic;

namespace PocketSharp.Models
{
  public class ModifyParameters : ParameterBase
  {
    public List<ActionParameter> Actions { get; set; }


    public List<Parameter> Convert()
    {
      List<Parameter> parameters = new List<Parameter>();
      List<object> actions = new List<object>();

      Actions.ForEach(delegate(ActionParameter action)
      {
        actions.Add(action.Convert());
      });

      parameters.Add(CreateParam("actions", JsonSerializer.SerializeToString(actions)));

      return parameters;
    }
  }
}
