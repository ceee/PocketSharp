﻿using PocketSharp.Models;
using System;
using System.Collections.Generic;

namespace PocketSharp
{
  /// <summary>
  /// PocketClient
  /// </summary>
  public partial class PocketClient
  {
    /// <summary>
    /// Adds a new item to pocket
    /// </summary>
    /// <param name="uri">The URL of the item you want to save</param>
    /// <param name="tags">A comma-separated list of tags to apply to the item</param>
    /// <param name="title">This can be included for cases where an item does not have a title, which is typical for image or PDF URLs. If Pocket detects a title from the content of the page, this parameter will be ignored.</param>
    /// <param name="tweetID">If you are adding Pocket support to a Twitter client, please send along a reference to the tweet status id. This allows Pocket to show the original tweet alongside the article.</param>
    /// <returns></returns>
    public PocketItem Add(Uri uri, string[] tags = null, string title = null, string tweetID = null)
    {
      AddParameters parameters = new AddParameters()
      {
        Uri = uri,
        Tags = tags,
        Title = title,
        TweetID = tweetID
      };
      return Get<Add>("add", parameters.Convert(), true).Item;
    }


    /// <summary>
    /// Adds a new item to pocket
    /// </summary>
    /// <param name="uri">The URL of the item you want to save</param>
    /// <returns></returns>
    public PocketItem Add(Uri uri)
    {
      return Add(uri, null, null, null);
    }
  }
}
