using System.Xml.Serialization;

namespace BrickLink.Scraper.Model.XmlData;

[XmlRoot(ElementName="IgnoredSellers")]
public class XmlIgnoredSellers
{
    [XmlElement("Seller")]
    public List<string>? Seller { get; set; }
}