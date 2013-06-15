using RestSharp;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketSharp.Models.Parameters
{
  public class RetrieveParameters : ParameterBase
  {
    public StateEnum? State { get; set; }

    public bool? Favorite { get; set; }

    public string Tag { get; set; }

    public ContentTypeEnum? ContentType { get; set; }

    public SortEnum? Sort { get; set; }


    public List<Parameter> Convert()
    {
      List<Parameter> parameters = new List<Parameter>();

      if(State != null)
      {
        parameters.Add(CreateParam("state", State.ToString()));
      }
      if(Favorite != null)
      {
        parameters.Add(CreateParam("favorite", (bool)Favorite ? "1" : "0"));
      }

      return parameters;
    }
  }


  public enum StateEnum
  {
    unread,
    archive,
    all
  }

  public enum SortEnum
  {
    newest,
    oldest,
    title,
    site
  }

  public enum ContentTypeEnum
  {
    article,
    video,
    image
  }
}
