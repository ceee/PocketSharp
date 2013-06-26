using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocketSharp
{
  internal class Utilities
  {
    public static int? GetUnixTimestamp(DateTime? dateTime)
    {
      if(dateTime == null) return null;

      return (int)((DateTime)dateTime - new DateTime(1970, 1, 1)).TotalSeconds;
    }


    public static Parameter CreateParam(string name, object value, ParameterType type = ParameterType.GetOrPost)
    {
      return new Parameter()
      {
        Name = name,
        Value = value,
        Type = type
      };
    }
  }
}
