using PocketSharp.Ports.NReadability;
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
    protected readonly HttpClient _httpClient;


    /// <summary>
    /// Initializes a new instance of the <see cref="PocketReader"/> class.
    /// </summary>
    public PocketReader()
    {
      // initialize HTTP client
      _httpClient = new HttpClient(new HttpClientHandler()
      {
        AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
        AllowAutoRedirect = true
      });

      // add accept types
      _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");

      // add accepted encodings
      _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip,deflate");

      // add user agent (default for Opera with PocketSharp identifier appended)
      _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.4 Safari/537.36 OPR/18.0.1284.2 PocketSharp/2.0");
    }



    /// <summary>
    /// Reads article content from the given URI.
    /// This method does not use the official Article View API, which is private.
    /// The PocketReader is based on a custom PCL port of NReadability and SgmlReader.
    /// </summary>
    /// <param name="uri">An URI.</param>
    /// <returns>A Pocket article with extracted content and title.</returns>
    /// <exception cref="PocketRequestException"></exception>
    public async Task<PocketArticle> Read(Uri uri)
    {
      return await Read(new PocketItem()
      {
        ID = 0,
        Uri = uri
      });
    }



    /// <summary>
    /// Reads article content from the given PocketItem.
    /// This method does not use the official Article View API, which is private.
    /// The PocketReader is based on a custom PCL port of NReadability and SgmlReader.
    /// </summary>
    /// <param name="item">The pocket item.</param>
    /// <returns>A Pocket article with extracted content and title.</returns>
    /// <exception cref="PocketRequestException"></exception>
    public async Task<PocketArticle> Read(PocketItem item)
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
      string htmlResponse = await Request(item.Uri);

      // set properties for processing
      TranscodingInput transcodingInput = new TranscodingInput(htmlResponse)
      {
        Url = item.Uri.ToString(),
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
      
      return new PocketArticle()
      {
        Content = transcodingResult.ExtractedContent,
        Title = transcodingResult.ExtractedTitle,
        NextPage = transcodingResult.NextPageUrl != null ? new Uri(transcodingResult.NextPageUrl, UriKind.Absolute) : null,
        PocketItemID = item.ID
      };
    }



    /// <summary>
    /// Fetches a resource
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <returns></returns>
    protected async Task<string> Request(Uri uri)
    {
      HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
      HttpResponseMessage response = null;

      // make async request
      try
      {
        response = await _httpClient.SendAsync(request);
      }
      catch (HttpRequestException exc)
      {
        throw new PocketException(exc.Message, exc);
      }

      // validate HTTP response
      if (response.StatusCode != HttpStatusCode.OK)
      {
        string exceptionString = String.Format("Request error: {0} ({1})", response.ReasonPhrase, (int)response.StatusCode);

        throw new PocketException(exceptionString);
      }

      // read response
      return await response.Content.ReadAsStringAsync();
    }
  }
}