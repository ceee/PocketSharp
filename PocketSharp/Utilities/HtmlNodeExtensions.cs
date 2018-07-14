using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketSharp
{
  internal static class HtmlNodeExtensions
  {
    public static HtmlNodeCollection SelectNodesByClass(this HtmlNode node, string className, bool isRoot = false)
    {
      string prefix = isRoot ? "//" : "";
      return node.SelectNodes($"{prefix}*[contains(@class, '{className}')]");
    }


    public static HtmlNode SelectNodeByClass(this HtmlNode node, string className, bool isRoot = false)
    {
      string prefix = isRoot ? "//" : "";
      return node.SelectSingleNode($"{prefix}*[contains(@class, '{className}')]");
    }
  }
}
