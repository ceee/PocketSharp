using RestSharp;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  public class ActionParameter : ParameterBase
  {
    public string Action { get; set; }

    public int ID { get; set; }

    public DateTime? Time { get; set; }


    public object Convert()
    {
      Dictionary<string, object> parameters = new Dictionary<string, object>
      {
        { "item_id", ID.ToString() },
        { "action", Action }
      };

      if(Time != null)
        parameters.Add( "time", (int)((DateTime)Time - new DateTime(1970, 1, 1)).TotalSeconds );

      return parameters;
    }
  }
}
