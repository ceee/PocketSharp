using RestSharp;
using RestSharp.Deserializers;
using ServiceStack.Text;

namespace PocketSharp
{
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
      var x = JsonSerializer.DeserializeFromString<T>(response.Content);
      return x;
    }

    public string DateFormat { get; set; }

    public string Namespace { get; set; }

    public string RootElement { get; set; }

    public string ContentType
    {
      get { return JsonContentType; }
    }
  }
}
