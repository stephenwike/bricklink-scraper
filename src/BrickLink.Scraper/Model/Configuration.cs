using System.Xml.Serialization;
using BrickLink.Scraper.Model.XmlData;

namespace BrickLink.Scraper.Model;

[XmlRoot("Configuration")]
public static class Configuration
{
    /// <summary>
    /// The length in hours to keep cache files before cleaning them out.
    /// </summary>
    public static double CacheTimeHoursPolicy = 24;
    
    /// <summary>
    /// Millisecond given to load webpage before chromedriver captures the html.
    /// </summary>
    public static int HtmlLoadTimeMs = 250;

    /// <summary>
    /// List of preferred Sellers.
    /// </summary>
    public static List<string> PreferredSellers = new List<string>();
    
    /// <summary>
    /// List of ignored Sellers.
    /// </summary>
    public static List<string> IgnoredSellers = new List<string>();

    /// <summary>
    /// Object contains the weights used to sort sellers. 
    /// </summary>
    public static XmlSellerWeights SellerWeights = new XmlSellerWeights
        { PricePoint = 1.2, Quantity = 1.0, Rarity = 1.5 };

    /// <summary>
    /// The number of sellers to return when compiling csv.
    /// </summary>
    public static int NumberOfSellers = 40;
    
    /// <summary>
    /// Enumeration that determine which property to sort by.
    /// </summary>
    public static int SortPartBy = 1;

    /// <summary>
    /// Number of part results loaded per page.
    /// </summary>
    public static int ResultsPerPage = 100;

    /// <summary>
    /// Percent of quantity of needed parts seller must have.
    /// Must be between 0 and 100.
    /// </summary>
    public static int MinQuantityPercent = 10;

    /// <summary>
    /// The name of the XML file containing the parts inventory.
    /// </summary>
    public static string InventoryFile = "Inventory.xml";
}