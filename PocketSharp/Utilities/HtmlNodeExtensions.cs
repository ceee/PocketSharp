//using HtmlAgilityPack;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace PocketSharp
//{
//  internal static class HtmlNodeExtensions
//  {
//    public static IEnumerable<HtmlNode> SelectNodesByClass(this HtmlNode node, string className)
//    {
//      return node.Descendants().Where(x => x.HasClass(className));
//    }


//    public static HtmlNode SelectNodeByClass(this HtmlNode node, string className)
//    {
//      return node.Descendants().FirstOrDefault(x => x.HasClass(className));
//    }
//  }
//}
