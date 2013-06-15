using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocketSharp.Models.Parameters
{
  public abstract class ParameterBase
  {
    protected Parameter CreateParam(string name, object value, ParameterType type = ParameterType.GetOrPost)
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
