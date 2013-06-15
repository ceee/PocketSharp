using PocketSharp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PocketSharp.Models.Parameters;

namespace PocketSharp
{
  public partial class PocketClient
  {
    public bool Retrieve(RetrieveParameters para)
    {
      //var parameters = new List<Parameter>()
      //{
      //  new Parameter { Name = "favorite", Value = "1", Type = ParameterType.GetOrPost }
      //};

      var parameters = new RetrieveParameters()
      {
        Favorite = true,
        State = StateEnum.all
      };

      var paramss = parameters.Convert();

      Retrieve result = GetResource<Retrieve>("get", parameters.Convert());

      return true;
    }
  }
}
