using System.Xml.Serialization;

namespace BrickLink.Scraper.Model.XmlData;

[XmlRoot("INVENTORY")]
public class XmlInventory
{
    [XmlElement("ITEM")] public List<XmlItem>? Items { get; set; }
}