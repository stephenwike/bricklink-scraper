using System.Web;
using BrickLink.Scraper.Helpers;
using BrickLink.Scraper.Model;
using HtmlAgilityPack;

namespace BrickLink.Scraper;

public class WebScraper
{
    public List<ItemScrape>? FileScrape(string fileName)
    {
        var url = Path.Combine(Environment.CurrentDirectory, Constants.HtmlFilesDirectory, fileName);
        var doc = new HtmlDocument();
        doc.Load(url);
        
        var body = doc.DocumentNode.SelectSingleNode("//body");
        var table = body.GetChild("div", 2).FirstChild("center")
            .FirstChild("table").FirstChild("tbody").FirstChild("tr").FirstChild("td")
            .FirstChild("section").FirstChild("div").FirstChild("div").FirstChild("div")
            .GetChild("div", 2).FirstChild("table").FirstChild("tbody");
        var rows = table.GetChildren("tr");

        var count = rows.Count;
        rows = rows.Skip(2).Take(count - 3).ToList();

        var items = rows.Select(row =>
        {
            var data = row.GetChildren("td");
            return new ItemScrape
            {
                Description = data[1].FirstChild("a").FirstChild("b").InnerText,
                Quantity = data[2].FirstChild("b").InnerText,
                Seller = TextHelper.HtmlDecode(data[3].FirstChild("a").InnerText),
                Link = data[3].FirstChild("a").InnerText,
                Price = data[4].FirstChild("span").InnerText,
            };
        }).ToList();
        
        return items;
    }
}