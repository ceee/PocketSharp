using NReadability;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PocketSharp.Models;

namespace PocketSharp
{
  /// <summary>
  /// PocketReader
  /// </summary>
  public class PocketReader
  {
    /// <summary>
    /// REST client used for HTML retrieval
    /// </summary>
    protected readonly HttpClient _restClient;


    /// <summary>
    /// Initializes a new instance of the <see cref="PocketReader"/> class.
    /// </summary>
    public PocketReader()
    {
      // initialize REST client
      _restClient = new HttpClient(new HttpClientHandler()
      {
        AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
      });

      _restClient.DefaultRequestHeaders.Add("Accept", "text/html");
    }



    /// <summary>
    /// Reads content from the given URI.
    /// </summary>
    /// <param name="item">The pocket item.</param>
    /// <returns></returns>
    public async Task<string> Read(PocketItem item)
    {
      return await Read(item.Uri);
    }



    /// <summary>
    /// Reads content from the given URI.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <returns></returns>
    public async Task<string> Read(Uri uri)
    {
      // initialize transcoder
      NReadabilityTranscoder transcoder = new NReadabilityTranscoder(
        dontStripUnlikelys: false,
        dontNormalizeSpacesInTextContent: true,
        dontWeightClasses: false,
        readingStyle: ReadingStyle.Ebook,
        readingMargin: ReadingMargin.Narrow,
        readingSize: ReadingSize.Medium
      );

      // get HTML string from URI
      string htmlResponse = await Request(uri);

      // set properties for processing
      TranscodingInput transcodingInput = new TranscodingInput(htmlResponse)
      {
        Url = uri.ToString(),
        DomSerializationParams = new DomSerializationParams()
        {
          BodyOnly = true,
          PrettyPrint = true,
          DontIncludeContentTypeMetaElement = true,
          DontIncludeMobileSpecificMetaElements = true,
          DontIncludeDocTypeMetaElement = true,
          DontIncludeGeneratorMetaElement = true
        }
      };

      // process/transcode HTML
      TranscodingResult transcodingResult = transcoder.Transcode(transcodingInput);
      
      return transcodingResult.ExtractedContent;
    }



    /// <summary>
    /// Fetches a resource
    /// </summary>
    protected async Task<string> Request(Uri uri)
    {
      HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);

      // make async request
      HttpResponseMessage response = await _restClient.SendAsync(request);

      // validate HTTP response
      ValidateResponse(response);

      // read response
      var responseString = await response.Content.ReadAsStringAsync();

      return responseString;
    }


    /// <summary>
    /// Validates the response.
    /// </summary>
    /// <param name="response">The response.</param>
    /// <returns></returns>
    /// <exception cref="PocketException">
    /// Error retrieving response
    /// </exception>
    protected void ValidateResponse(HttpResponseMessage response)
    {
      // no error found
      if (response.StatusCode == HttpStatusCode.OK)
      {
        return;
      }

      string exceptionString = String.Format("Request error: {0} ({1})", response.ReasonPhrase, (int)response.StatusCode);

      throw new PocketException(exceptionString);
    }
  }
}