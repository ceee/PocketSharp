using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketSharp.Models
{
  /// <summary>
  /// Modify Response
  /// </summary>
  [DataContract]
  class Modify : ResponseBase
  {
    /// <summary>
    /// Gets or sets the action results.
    /// </summary>
    /// <value>
    /// The action results.
    /// </value>
    [DataMember(Name = "action_results")]
    public bool[] ActionResults { get; set; }
  }
}
