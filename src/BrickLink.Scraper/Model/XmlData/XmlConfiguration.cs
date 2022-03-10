using System.Xml.Serialization;

namespace BrickLink.Scraper.Model.XmlData;

[XmlRoot("Configuration")]
public  class XmlConfiguration
{
    [XmlElement("CacheTimeHoursPolicy")]
    public double CacheTimeHoursPolicy = 24;

    [XmlElement("HtmlLoadTimeMs")]
    public int HtmlLoadTimeMs = 250;

    [XmlElement(ElementName="PreferredSellers")]
    public XmlPreferredSellers? PreferredSellers { get; set; } 

    [XmlElement(ElementName="IgnoredSellers")]
    public XmlIgnoredSellers? IgnoredSellers { get; set; }
    
    [XmlElement(ElementName="SellerWeights")]
    public XmlSellerWeights? SellerWeights { get; set; }

    [XmlElement(ElementName = "MinQuantityPercent")]
    public int MinQuantityPercent = 10;

    [XmlElement(ElementName = "NumberOfSellers")]
    public int NumberOfSellers = 40;

    [XmlElement(ElementName = "SortPartsBy")]
    public int SortPartsBy = 1;
    
    [XmlElement(ElementName = "ResultsPerPage")]
    public int ResultsPerPage = 100;

    [XmlElement(ElementName = "InventoryFile")]
    public string InventoryFile = "Inventory.xml";
}