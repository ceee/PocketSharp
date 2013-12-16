using PropertyChanged;
using System;
using System.Collections.Generic;

namespace PocketSharp.Models
{
  /// <summary>
  /// Readable article
  /// </summary>
  [ImplementPropertyChanged]
  public class PocketArticle
  {
    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    /// <value>
    /// The content.
    /// </value>
    public string Content { get; set; }

    /// <summary>
    /// Gets or sets the images.
    /// </summary>
    /// <value>
    /// The images.
    /// </value>
    public List<PocketArticleImage> Images { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the next page URL.
    /// </summary>
    /// <value>
    /// The next page URL.
    /// </value>
    public Uri NextPage { get; set; }
  }
}
