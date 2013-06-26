using RestSharp;
using RestSharp.Deserializers;
using ServiceStack.Text;
using System;

namespace PocketSharp
{
  /// <summary>
  /// Custom JSON Deserializer which implements ServiceStack.Text
  /// </summary>
  internal class JsonDeserializer : IDeserializer
  {
    public const string JsonContentType = "application/json";

    /// <summary>
    /// Deserializes the specified response.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response">The response.</param>
    /// <returns></returns>
    public T Deserialize<T>(IRestResponse response)
    {
      return JsonSerializer.DeserializeFromString<T>(response.Content);
    }


    /// <summary>
    /// Adds custom deserialization for specific types.
    /// </summary>
    public static void AddCustomDeserialization()
    {
      // generate correct Uri format
      JsConfig<Uri>.DeSerializeFn = value => new Uri(value);

      // create DateTime from UNIX timestamp input
      JsConfig<DateTime?>.DeSerializeFn = value =>
      {
        if (value == "0") return null;
        return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToDouble(value)).ToLocalTime();
      };
    }


    /// <summary>
    /// Gets or sets the date format.
    /// </summary>
    /// <value>
    /// The date format.
    /// </value>
    public string DateFormat { get; set; }

    /// <summary>
    /// Gets or sets the namespace.
    /// </summary>
    /// <value>
    /// The namespace.
    /// </value>
    public string Namespace { get; set; }

    /// <summary>
    /// Gets or sets the root element.
    /// </summary>
    /// <value>
    /// The root element.
    /// </value>
    public string RootElement { get; set; }

    /// <summary>
    /// Gets the type of the content.
    /// </summary>
    /// <value>
    /// The type of the content.
    /// </value>
    public string ContentType
    {
      get { return JsonContentType; }
    }
  }
}
