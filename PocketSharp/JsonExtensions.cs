using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocketSharp
{
  //public class UnixDateTimeConverter : DateTimeConverterBase
  //{
  //  /// <summary>
  //  /// Writes the JSON representation of the object.
  //  /// </summary>
  //  /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param><param name="value">The value.</param><param name="serializer">The calling serializer.</param>
  //  //public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
  //  //{
  //  //  DateTime epoc = new DateTime(1970, 1, 1);
  //  //  var delta = (DateTime)value - epoc;

  //  //  writer.WriteValue((long)delta.TotalSeconds);
  //  //}


  //  ///// <summary>
  //  ///// Reads the JSON representation of the object.
  //  ///// </summary>
  //  ///// <param name="reader">The <see cref = "JsonReader" /> to read from.</param>
  //  ///// <param name="objectType">Type of the object.</param>
  //  ///// <param name="existingValue">The existing value of object being read.</param>
  //  ///// <param name="serializer">The calling serializer.</param>
  //  ///// <returns>The object value.</returns>
  //  //public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
  //  //{
  //  //  return existingValue == "0" ? null : new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToDouble(existingValue)).ToLocalTime();
  //  //}
  //}
}
