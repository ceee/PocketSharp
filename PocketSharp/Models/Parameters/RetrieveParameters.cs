using RestSharp;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  public class RetrieveParameters : ParameterBase
  {
    public StateEnum? State { get; set; }

    public bool? Favorite { get; set; }

    public string Tag { get; set; }

    public ContentTypeEnum? ContentType { get; set; }

    public SortEnum? Sort { get; set; }

    public DetailTypeEnum? DetailType { get; set; }

    public string Search { get; set; }

    public string Domain { get; set; }

    public DateTime? Since { get; set; }

    public int? Count { get; set; }

    public int? Offset { get; set; }


    public List<Parameter> Convert()
    {
      List<Parameter> parameters = new List<Parameter>();

      if (State != null) 
        parameters.Add(CreateParam("state", State.ToString()));
      if (Favorite != null) 
        parameters.Add(CreateParam("favorite", (bool)Favorite ? "1" : "0"));
      if (Tag != null) 
        parameters.Add(CreateParam("tag", Tag));
      if (ContentType != null) 
        parameters.Add(CreateParam("contentType", ContentType.ToString()));
      if (Sort != null) 
        parameters.Add(CreateParam("sort", Sort.ToString()));
      if (DetailType != null) 
        parameters.Add(CreateParam("detailType", DetailType.ToString()));
      if (Search != null) 
        parameters.Add(CreateParam("search", Search));
      if (Domain != null) 
        parameters.Add(CreateParam("domain", Domain));
      if (Since != null)
        parameters.Add(CreateParam("since", (int)((DateTime)Since - new DateTime(1970, 1, 1)).TotalSeconds));
      if (Count != null)
        parameters.Add(CreateParam("count", Count));
      if (Offset != null)
        parameters.Add(CreateParam("offset", Offset));

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

  public enum DetailTypeEnum
  {
    simple,
    complete
  }
}
