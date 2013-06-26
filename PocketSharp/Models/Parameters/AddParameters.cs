using RestSharp;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  public class AddParameters : ParameterBase
  {
    public Uri Uri { get; set; }

    public string Title { get; set; }

    public string[] Tags { get; set; }

    public string TweetID { get; set; }

    public List<Parameter> Convert()
    {
      List<Parameter> parameters = new List<Parameter>();

      if (Uri != null)
        parameters.Add(CreateParam("url", Uri.ToString()));
      if (Title != null)
        parameters.Add(CreateParam("title", Title));
      if (Tags != null)
        parameters.Add(CreateParam("tags", String.Join(",", Tags)));
      if (TweetID != null)
        parameters.Add(CreateParam("tweet_id", TweetID));

      return parameters;
    }
  }
}
