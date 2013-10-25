using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections;

namespace PocketSharp.Models
{
  /// <summary>
  /// Parameter
  /// </summary>
  internal class Parameters
  {

    /// <summary>
    /// Converts an object to a list of HTTP Post parameters.
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, string> Convert()
    {
      // store HTTP parameters here
      Dictionary<string, string> parameterDict = new Dictionary<string, string>();

      // get object properties
      IEnumerable<PropertyInfo> properties = this.GetType()
        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        .Where(p => Attribute.IsDefined(p, typeof(DataMemberAttribute)));

      // gather attributes of object
      foreach (PropertyInfo propertyInfo in properties)
      {
        DataMemberAttribute attribute = (DataMemberAttribute)propertyInfo.GetCustomAttributes(typeof(DataMemberAttribute‌), false).FirstOrDefault();
        string name = attribute.Name ?? propertyInfo.Name.ToLower();
        object value = propertyInfo.GetValue(this, null);

        // invalid parameter
        if (value == null)
        {
          continue;
        }

        // convert array to comma-seperated list
        if (value is IEnumerable && value.GetType().GetElementType() == typeof(string))
        {
          value = string.Join(",", ((IEnumerable)value).Cast<object>().Select(x => x.ToString()).ToArray());
        }

        // convert booleans
        if (value is bool)
        {
          value = System.Convert.ToBoolean(value) ? "1" : "0";
        }

        // convert DateTime to UNIX timestamp
        if (value is DateTime)
        {
          value = (int)((DateTime)value - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        parameterDict.Add(name, value.ToString());
      }

      return parameterDict;
    }
  }
}