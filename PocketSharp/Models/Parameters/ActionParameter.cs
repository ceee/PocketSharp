using RestSharp;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  public class ActionParameter
  {
    public string Action { get; set; }

    public int ID { get; set; }

    public DateTime? Time { get; set; }

    // specific params

    public string[] Tags { get; set; }

    public string OldTag { get; set; }

    public string NewTag { get; set; }


    public object Convert()
    {
      Dictionary<string, object> parameters = new Dictionary<string, object>
      {
        { "item_id", ID },
        { "action", Action }
      };

      if (Time != null)
        parameters.Add("time", Utilities.GetUnixTimestamp(Time));
      if (Tags != null)
        parameters.Add("tags", String.Join(",", Tags));
      if (OldTag != null)
        parameters.Add("old_tag", OldTag);
      if (NewTag != null)
        parameters.Add("new_tag", NewTag);

      return parameters;
    }
  }
}
