
using RestSharp;
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
    /// <returns>UNIX timestamp</returns>
    public static int? GetUnixTimestamp(DateTime? dateTime)
    {
      if (dateTime == null)
      {
        return null;
      }

      return (int)((DateTime)dateTime - new DateTime(1970, 1, 1)).TotalSeconds;
    }


    /// <summary>
    /// Creates a Parameter object.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="value">The value.</param>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static Parameter CreateParam(string name, object value, ParameterType type = ParameterType.GetOrPost)
    {
      return new Parameter() { Name = name, Value = value, Type = type };
    }


    /// <summary>
    /// Creates a Parameter object within a list.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="value">The value.</param>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static List<Parameter> CreateParamInList(string name, object value, ParameterType type = ParameterType.GetOrPost)
    {
      return new List<Parameter>() { CreateParam(name, value, type) };
    }


    /// <summary>
    /// Convert a dictionary to a list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    /// <returns></returns>
    public static List<T> DictionaryToList<T>(Dictionary<string, T> items) where T : new()
    {
      if (items == null)
      {
        return null;
      }

      var itemEnumerator = items.GetEnumerator();
      List<T> list = new List<T>();

      while (itemEnumerator.MoveNext())
      {
        list.Add(itemEnumerator.Current.Value);
      }

      return list;
    }
  }
}
