﻿using Newtonsoft.Json;
using System;
using PropertyChanged;

namespace PocketSharp.Models
{
  /// <summary>
  /// Image
  /// </summary>
  [JsonObject]
  [ImplementPropertyChanged]
  public class PocketImage
  {
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    /// <value>
    /// The ID.
    /// </value>
    [JsonProperty("image_id")]
    public string ID { get; set; }

    /// <summary>
    /// Gets or sets the caption.
    /// </summary>
    /// <value>
    /// The caption.
    /// </value>
    [JsonProperty]
    public string Caption { get; set; }

    /// <summary>
    /// Gets or sets the credit.
    /// </summary>
    /// <value>
    /// The credit.
    /// </value>
    [JsonProperty]
    public string Credit { get; set; }

    /// <summary>
    /// Gets or sets the URI.
    /// </summary>
    /// <value>
    /// The URI.
    /// </value>
    [JsonProperty("src")]
    public Uri Uri { get; set; }
  }
}
