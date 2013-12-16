using PocketSharp.Models;
using PocketSharp.Ports.NReadability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PocketSharp
{
  /// <summary>
  /// PocketReader
  /// </summary>
  public class PocketReader : IPocketReader
  {
    /// <summary>
    /// Used UserAgent for HTTP request
    /// </summary>
    protected string _userAgent = "Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0; ARM; Mobile; Touch{0}) like Gecko";

    /// <summary>
    /// REST client used for HTML retrieval
    /// </summary>
    protected readonly HttpClient _httpClient;


    /// <summary>
    /// Initializes a new instance of the <see cref="PocketReader" /> class.
    /// </summary>
    /// <param name="userAgent">Custom UserAgent string.</param>
    /// <param name="handler">The HttpMessage handler.</param>
    /// <param name="timeout">Request timeout (in seconds).</param>
    public PocketReader(string userAgent = null, HttpMessageHandler handler = null, int? timeout = null)
    {
      // override user agent
      if (!string.IsNullOrEmpty(userAgent))
      {
        _userAgent = userAgent;
      }

      // initialize HTTP client
      _httpClient = new HttpClient(handler ?? new HttpClientHandler()
      {
        AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
        AllowAutoRedirect = true
      });

      if (timeout.HasValue)
      {
        _httpClient.Timeout = TimeSpan.FromSeconds(timeout.Value);
      }

      // add accept types
      _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");

      // add accepted encodings
      _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip,deflate");

      // add user agent
      _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", string.Format(_userAgent, "; PocketSharp/3.0"));
    }



    /// <summary>
    /// Reads article content from the given PocketItem.
    /// This method does not use the official Article View API, which is private.
    /// The PocketReader is based on a custom PCL port of NReadability and SgmlReader.
    /// </summary>
    /// <param name="uri">An URI to extract the content from.</param>
    /// <param name="bodyOnly">if set to <c>true</c> [only body is returned].</param>
    /// <param name="noHeadline">if set to <c>true</c> [no headline (h1) is included].</param>
    /// <returns>
    /// A Pocket article with extracted content and title.
    /// </returns>
    /// <exception cref="Exception"></exception>
    public async Task<PocketArticle> Read(Uri uri, bool bodyOnly = true, bool noHeadline = false)
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
          BodyOnly = bodyOnly,
          NoHeadline = noHeadline,
          PrettyPrint = true,
          DontIncludeContentTypeMetaElement = true,
          DontIncludeMobileSpecificMetaElements = true,
          DontIncludeDocTypeMetaElement = false,
          DontIncludeGeneratorMetaElement = true
        }
      };

      // process/transcode HTML
      TranscodingResult transcodingResult = transcoder.Transcode(transcodingInput);

      // get images from article
      List<PocketArticleImage> images = transcodingResult.Images.Select<XElement, PocketArticleImage>(image =>
      {
        Uri imageUri;
        Uri.TryCreate(image.GetAttributeValue("src", null), UriKind.Absolute, out imageUri);

        return new PocketArticleImage()
        {
          Uri = imageUri,
          Title = image.GetAttributeValue("title", null),
          AlternativeText = image.GetAttributeValue("alt", null)
        };
      }).ToList();

      // create article
      return new PocketArticle()
      {
        Content = transcodingResult.ExtractedContent,
        Images = images,
        Title = transcodingResult.ExtractedTitle,
        NextPage = transcodingResult.NextPageUrl != null ? new Uri(transcodingResult.NextPageUrl, UriKind.Absolute) : null
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
        throw new Exception(exc.Message, exc);
      }

      // validate HTTP response
      if (response.StatusCode != HttpStatusCode.OK)
      {
        string exceptionString = String.Format("Request error: {0} ({1})", response.ReasonPhrase, (int)response.StatusCode);

        throw new Exception(exceptionString);
      }

      // read response
      return await response.Content.ReadAsStringAsync();
    }
  }
}