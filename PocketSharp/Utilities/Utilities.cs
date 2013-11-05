using System;
using System.Collections.Generic;

namespace PocketSharp
{
  /// <summary>
  /// General utilities
  /// </summary>
  internal class Utilities
  {
    /// <summary>
    /// converts DateTime to an UNIX timestamp
    /// </summary>
    /// <param name="dateTime">The date.</param>
    /// <returns>
    /// UNIX timestamp
    /// </returns>
    public static int? GetUnixTimestamp(DateTime? dateTime)
    {
      if (dateTime == null)
      {
        return null;
      }

      return (int)((DateTime)dateTime - new DateTime(1970, 1, 1)).TotalSeconds;
    }
  }
}
