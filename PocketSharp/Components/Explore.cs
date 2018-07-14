using HtmlAgilityPack;
using PocketSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace PocketSharp
{
  /// <summary>
  /// PocketClient
  /// </summary>
  public partial class PocketClient
  {
    /// <summary>
    /// Term to use for trending articles in the Explore() method
    /// </summary>
    public const string EXPLORE_TRENDING = "trending";

    /// <summary>
    /// Term to use for must-read articles in the Explore() method
    /// </summary>
    public const string EXPLORE_MUST_READS = "must-reads";


    /// <summary>
    /// Explore Pocket and find interesting articles by a certain topic
    /// </summary>
    /// <param name="topic">Term or topic to get articles for</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<PocketItem>> Explore(string topic, CancellationToken cancellationToken = default(CancellationToken))
    {
      List<PocketItem> items = new List<PocketItem>();
      string html = await RequestAsString("https://getpocket.com/explore/" + HttpUtility.UrlEncode(topic) , cancellationToken);

      var document = new HtmlDocument();
      document.LoadHtml(html);

      HtmlNodeCollection nodes = document.DocumentNode.SelectNodesByClass("media_item", true);

      if (nodes == null || nodes.Count == 0)
      {
        return items;
      }

      for (int i = 0; i < nodes.Count; i++)
      {
        HtmlNode node = nodes[i];
        PocketItem item = new PocketItem();
        item.ID = node.Id;

        HtmlNode title = node.Descendants().FirstOrDefault(x => x.HasClass("title"))?.FirstChild;

        if (title == null)
        {
          continue;
        }

        // get uri
        string uri = title.GetAttributeValue("data-saveurl", null);
        item.Uri = new Uri(uri);

        // get image        
        string imageUri = node.SelectNodeByClass("item_image")?.GetAttributeValue("data-thumburl", null);
        if (!String.IsNullOrEmpty(imageUri))
        {
          PocketImage image = new PocketImage();
          image.Uri = new Uri(imageUri);
          image.ID = "0";
          image.ItemID = item.ID;
          item.Images = new List<PocketImage>() { image };
        }

        // get basic infos
        item.Title = title.InnerText;
        item.Excerpt = node.SelectNodeByClass("excerpt")?.InnerText;
        item.IsTrending = node.SelectNodeByClass("flag_trending") != null;

        // add published date
        DateTime publishedDate = DateTime.Now;
        if (DateTime.TryParse(node.SelectNodeByClass("read_time")?.InnerText, out publishedDate))
        {
          item.PublishedTime = publishedDate;
        }

        items.Add(item);
      }

      return items;
    }
  }
}
