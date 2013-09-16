using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Linq;

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

        if (propertyInfo.GetValue(this, null) != null)
        {
          parameterDict.Add(name, propertyInfo.GetValue(this, null).ToString());
        }
      }

      return parameterDict;
    }
  }
}