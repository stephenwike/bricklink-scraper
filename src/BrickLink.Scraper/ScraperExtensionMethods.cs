using HtmlAgilityPack;

namespace BrickLink.Scraper;

public static class ScraperExtensionMethods
{
    public static HtmlNode FirstChild(this HtmlNode node, string tagName = "*")
    {
        return node.ChildNodes.Where(x => x.Name == tagName).ToList().First();
    }

    public static HtmlNode GetChild(this HtmlNode node, string tagName, int position = 0)
    {
        return node.ChildNodes.Where(x => x.Name == tagName).ToList()[position];
    }

    public static IList<HtmlNode> GetChildren(this HtmlNode node, string tagName = "*")
    {
        return node.ChildNodes.Where(x => x.Name == tagName).ToList();
    }
}