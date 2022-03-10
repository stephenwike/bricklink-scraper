using System.Xml.Serialization;

namespace BrickLink.Scraper.Model.XmlData;

[XmlRoot(ElementName="PreferredSellers")]
public class XmlPreferredSellers
{
    [XmlElement("Seller")]
    public List<string>? Seller { get; set; }
}