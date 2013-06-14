using PocketSharp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocketSharp
{
  public partial class PocketClient : IPocketClient
  {
    public bool Get()
    {
      //var parameters = new List<Parameter>()
      //{
      //  new Parameter { Name = "favorite", Value = "1", Type = ParameterType.GetOrPost }
      //};

      Retrieve result = GetResource<Retrieve>("get", parameters);

      return true;
    }
  }
}
