﻿using PocketSharp.Ports.NReadability;
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
        AllowAutoRedirect = true,
        MaxAutomaticRedirections = 10
      });

      // add accept types
      _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");

      // add accepted encodings
      _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip,deflate");

      // add user agent (default for Opera with PocketSharp identifier appended)
      _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.4 Safari/537.36 OPR/18.0.1284.2 PocketSharp/2.0");
    }



    /// <summary>
    /// Reads content from the given URI.
    /// </summary>
    /// <param name="item">The pocket item.</param>
    /// <returns></returns>
    /// <exception cref="PocketException"></exception>
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
        NextPageUrl = transcodingResult.NextPageUrl,
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

      // make async request
      HttpResponseMessage response = await _httpClient.SendAsync(request);

      // validate HTTP response
      if (response.StatusCode != HttpStatusCode.OK)
      {
        string exceptionString = String.Format("Request error: {0} ({1})", response.ReasonPhrase, (int)response.StatusCode);

        throw new PocketException(exceptionString);
      }

      // read response
      var responseString = await response.Content.ReadAsStringAsync();

      return responseString;
    }
  }
}