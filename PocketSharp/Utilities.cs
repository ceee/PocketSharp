using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocketSharp
{
  /// <summary>
  /// General utilities
  /// </summary>
  internal class Utilities
  {
    /// <summary>
    /// converts DateTime to an UNIX timestamp
    /// </summary>
    /// <param name="dateTime">The date.</param>
    /// <returns>UNIX timestamp</returns>
    public static int? GetUnixTimestamp(DateTime? dateTime)
    {
      if(dateTime == null) return null;

      return (int)((DateTime)dateTime - new DateTime(1970, 1, 1)).TotalSeconds;
    }


    /// <summary>
    /// Creates a Parameter object.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="value">The value.</param>
    /// <param name="type">The type.</param>
    /// <returns></returns>
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
