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
      IEnumerable<MemberInfo> properties = this.GetType()
        .GetTypeInfo()
        .DeclaredMembers
        .Where(p => p.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(DataMemberAttribute)) != null);

      // gather attributes of object
      foreach (MemberInfo memberInfo in properties)
      {
        DataMemberAttribute attribute = (DataMemberAttribute)memberInfo.GetCustomAttributes(typeof(DataMemberAttribute‌), false).FirstOrDefault();
        string name = attribute.Name ?? memberInfo.Name.ToLower();
        object value = null;

        if (memberInfo is FieldInfo)
        {
          value = ((FieldInfo)memberInfo).GetValue(this);
        }
        else
        {
          value = ((PropertyInfo)memberInfo).GetValue(this, null);
        }

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
          value = (int)((DateTime)value - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }

        parameterDict.Add(name, value.ToString());
      }

      return parameterDict;
    }
  }
}