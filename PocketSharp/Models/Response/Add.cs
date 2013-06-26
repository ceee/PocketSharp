using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  /// <summary>
  /// Add Response
  /// </summary>
  [DataContract]
  class Add : ResponseBase
  {
    /// <summary>
    /// Gets or sets the item.
    /// </summary>
    /// <value>
    /// The item.
    /// </value>
    [DataMember]
    public PocketItem Item { get; set; }
  }
}
