using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PocketSharp.Models
{
  /// <summary>
  /// Explore item containing all available data
  /// </summary>
  [JsonObject]
  [DebuggerDisplay("Uri = {Uri}, Title = {Title}")]
  public class PocketExploreItem : IComparable
  {
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    /// <value>
    /// The ID.
    /// </value>
    public string ID { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the excerpt.
    /// </summary>
    /// <value>
    /// The excerpt.
    /// </value>
    public string Excerpt { get; set; }

    /// <summary>
    /// Gets or sets the published time.
    /// </summary>
    /// <value>
    /// The time when the article was published.
    /// </value>
    public DateTime? PublishedTime { get; set; }

    /// <summary>
    /// Gets or sets the URI.
    /// </summary>
    /// <value>
    /// The URI.
    /// </value>
    [JsonIgnore]
    public Uri Uri { get; set; }

    /// <summary>
    /// Gets or sets the save count.
    /// </summary>
    /// <value>
    /// The save count.
    /// </value>
    public int SaveCount { get; set; }

    /// <summary>
    /// Gets or sets the images.
    /// </summary>
    /// <value>
    /// The images.
    /// </value>
    [JsonConverter(typeof(ObjectToArrayConverter<PocketImage>))]
    public IEnumerable<PocketImage> Images { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is trending.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is trending; otherwise, <c>false</c>.
    /// </value>
    public bool IsTrending { get; set; }

    /// <summary>
    /// Gets the lead image.
    /// </summary>
    /// <value>
    /// The lead image.
    /// </value>
    [JsonIgnore]
    public PocketImage LeadImage
    {
      get { return Images != null && Images.Count() > 0 ? Images.First() : null; }
    }

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>
    /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order.
    /// </returns>
    int IComparable.CompareTo(object obj)
    {
      PocketExploreItem item = (PocketExploreItem)obj;

      if (!PublishedTime.HasValue)
      {
        return 1;
      }
      if (!item.PublishedTime.HasValue)
      {
        return -1;
      }

      return DateTime.Compare(PublishedTime.Value, item.PublishedTime.Value);
    }

    /// <summary>
    /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj == null)
      {
        return false;
      }

      PocketExploreItem item = obj as PocketExploreItem;

      if (item == null)
      {
        return false;
      }

      return ID == item.ID;
    }


    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="a">A.</param>
    /// <param name="b">The b.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(PocketExploreItem a, PocketExploreItem b)
    {
      if (Object.ReferenceEquals(a, b))
      {
        return true;
      }

      PocketExploreItem itemA = (PocketExploreItem)a;
      PocketExploreItem itemB = (PocketExploreItem)b;

      if ((Object)itemA == null || (Object)itemB == null)
      {
        return false;
      }

      return itemA.ID == itemB.ID;
    }

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="a">A.</param>
    /// <param name="b">The b.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(PocketExploreItem a, PocketExploreItem b)
    {
      return !(a == b);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override int GetHashCode()
    {
      return ID.GetHashCode();
    }

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
      return ID;
    }
  }
}
