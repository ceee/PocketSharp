using RestSharp;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  public class AddParameters
  {
    public Uri Uri { get; set; }

    public string Title { get; set; }

    public string[] Tags { get; set; }

    public string TweetID { get; set; }

    public List<Parameter> Convert()
    {
      return new List<Parameter>()
      {
        Utilities.CreateParam("url", Uri.ToString() ),
        Utilities.CreateParam("title", Title),
        Utilities.CreateParam("tags", String.Join(",", Tags)),
        Utilities.CreateParam("tweet_id", TweetID)
      };
    }
  }
}
