using System.Xml.Serialization;

namespace BrickLink.Scraper.Model.XmlData;

public class XmlItem
{
    [XmlElement("ITEMID")] 
    public string? ItemId { get; set; }
    
    [XmlElement("ITEMTYPE")]
    public string? ItemType { get; set; }
    
    [XmlElement("COLOR")]
    public string? Color { get; set; }
    
    [XmlElement("MINQTY")]
    public string? MinQuantity { get; set; }
    
    [XmlElement("MAXPRICE")]
    public string? MaxPrice { get; set; }
    
    [XmlElement("CONDITION")]
    public string? Condition { get; set; }
}