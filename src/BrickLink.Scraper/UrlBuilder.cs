using System.Text;
using BrickLink.Scraper.Helpers;
using BrickLink.Scraper.Model;
using BrickLink.Scraper.Model.XmlData;

namespace BrickLink.Scraper;

public class UrlBuilder
{
    private const string Root = "https://www.bricklink.com/v2/catalog/catalogitem.page?P=";
    private const string Tsc = "#T=S&C=";
    private const string OptColor = "&O={\"color\":\"";
    private const string OptSortBy = "\",\"st\":\"";
    private const string OptCondition = "\",\"cond\":\"";
    private const string OptMaxPrice = "\",\"max\":\"";
    private const string OptMinQuantity = "\",\"minqty\":\"";
    private const string OptLocation = "\",\"loc\":\"US"; // Not configurable
    private const string OptResultsPerPage = "\",\"rpp\":\"";
    private const string OptIcOnly = "\",\"iconly\":0}"; // Not configurable.
    
    public string Build(XmlItem? item)
    {
        if (item == null)
            throw new Exception("An item evaluated to null while parsing the XML.  Null values are not allowed.");
        
        if (string.IsNullOrWhiteSpace(item.ItemId))
            throw new Exception("An item failed while parsing the XML. An item does not contain an ItemId.");

        if (string.IsNullOrWhiteSpace(item.Color))
            throw new Exception($"An item failed while parsing the XML. Item {item.ItemId}, does not contain a Color.");

        var maxPrice = ValueHelper.ParsePrice(item.MaxPrice) * 2.5;
        var minQuantity = ValueHelper.GetMinQuantityFromPercent(item.MinQuantity);

        var stringBuilder = new StringBuilder();
        stringBuilder.Append(Root)
            .Append(item.ItemId)
            .Append(Tsc)
            .Append(item.Color)
            .Append(OptColor)
            .Append(item.Color)
            .Append(OptSortBy)
            .Append(Configuration.SortPartBy)
            .Append(OptCondition)
            .Append(item.Condition ?? "N")
            .Append(OptMaxPrice)
            .Append(maxPrice)
            .Append(OptMinQuantity)
            .Append(minQuantity)
            .Append(OptLocation)
            .Append(OptResultsPerPage)
            .Append(Configuration.ResultsPerPage)
            .Append(OptIcOnly);

        return stringBuilder.ToString();
    }
}