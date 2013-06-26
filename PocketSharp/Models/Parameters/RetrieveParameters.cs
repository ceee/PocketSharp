using RestSharp;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  public class RetrieveParameters
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
      return new List<Parameter>()
      {
        Utilities.CreateParam("state", State != null ? State.ToString() : null ),
        Utilities.CreateParam("favorite", Favorite != null ? (bool)Favorite ? "1" : "0" : null),
        Utilities.CreateParam("tag", Tag),
        Utilities.CreateParam("contentType", ContentType != null ? ContentType.ToString() : null),
        Utilities.CreateParam("sort", Sort != null ? Sort.ToString() : null),
        Utilities.CreateParam("detailType", DetailType != null ? DetailType.ToString() : null),
        Utilities.CreateParam("search", Search),
        Utilities.CreateParam("domain", Domain),
        Utilities.CreateParam("since", Utilities.GetUnixTimestamp(Since)),
        Utilities.CreateParam("count", Count),
        Utilities.CreateParam("offset", Offset)
      };
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
