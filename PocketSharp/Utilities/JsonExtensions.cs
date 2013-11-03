using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace PocketSharp
{

  public class BoolConverter : JsonConverter
  {
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      writer.WriteValue(((bool)value) ? 1 : 0);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      return reader.Value == null ? false : (reader.Value.ToString() == "1");
    }

    public override bool CanConvert(Type objectType)
    {
      return objectType == typeof(bool);
    }
  }



  public class UnixDateTimeConverter : DateTimeConverterBase
  {
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      DateTime epoc = new DateTime(1970, 1, 1);
      var delta = (DateTime)value - epoc;

      writer.WriteValue((long)delta.TotalSeconds);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      if(reader.Value.ToString() == "0")
      {
        return null;
      }

      return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToDouble(reader.Value)).ToLocalTime();
    }
  }



  public class NullableIntConverter : JsonConverter
  {
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      writer.WriteValue(value);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      int result = 0;
      if (reader.Value != null)
      {
        result = Convert.ToInt32(reader.Value);
      }
      return result;
    }

    public override bool CanConvert(Type objectType)
    {
      return objectType == typeof(int);
    }
  }
}
