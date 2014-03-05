﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using PocketSharp.Models;

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
      if (reader.Value.ToString() == "0")
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



  public class UriConverter : JsonConverter
  {
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      if (reader.TokenType != JsonToken.String)
      {
        return null;
      }

      string value = reader.Value.ToString();

      if (Uri.IsWellFormedUriString(value, UriKind.Absolute))
      {
        return new Uri(value);
      }
      else if (value.StartsWith("//") && Uri.IsWellFormedUriString("http:" + value, UriKind.Absolute))
      {
        return new Uri("http:" + value);
      }
      else if (value.StartsWith("www.") && Uri.IsWellFormedUriString("http://" + value, UriKind.Absolute))
      {
        return new Uri("http://" + value);
      }

      return null;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      if (value == null)
      {
        writer.WriteNull();
      }
      else if (value is Uri)
      {
        writer.WriteValue(((Uri)value).OriginalString);
      }
    }

    public override bool CanConvert(Type objectType)
    {
      return objectType.Equals(typeof(Uri));
    }
  }



  public class ObjectToArrayConverter<T> : CustomCreationConverter<List<T>> where T : new()
  {
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      JObject jObject;
      List<T> result = new List<T>();
      T target;
      // object is an array
      if (reader.TokenType == JsonToken.StartArray)
      {
        return serializer.Deserialize<List<T>>(reader);
      }
      else if (reader.TokenType == JsonToken.Null)
      {
        return null;
      }

      try
      {
        jObject = JObject.Load(reader);
      }
      catch
      {
        return null;
      }

      // Populate the object properties
      foreach (KeyValuePair<string, JToken> item in jObject)
      {
        target = serializer.Deserialize<T>(item.Value.CreateReader());
        result.Add(target);
      }

      return result;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }

    public override List<T> Create(Type objectType)
    {
      return new List<T>();
    }

  }



  public class PocketItemConverter : CustomCreationConverter<PocketItem>
  {

    public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
    {

      var jObject = JObject.ReadFrom(reader);
      var pocketItem = new PocketItem();
      serializer.Populate(jObject.CreateReader(), pocketItem);
      pocketItem.Json = jObject.ToString();

      return pocketItem;
    }

    public override PocketItem Create(Type objectType)
    {
      return new PocketItem();
    }
  }
}
