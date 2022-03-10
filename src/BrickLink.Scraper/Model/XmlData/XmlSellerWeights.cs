using System.Xml.Serialization;

namespace BrickLink.Scraper.Model.XmlData;

[XmlRoot(ElementName="SellerWeights")]
public class XmlSellerWeights { 

    [XmlElement(ElementName="PricePoint")] 
    public double PricePoint { get; set; }

    [XmlElement(ElementName="Quantity")] 
    public double Quantity { get; set; }

    [XmlElement(ElementName="Rarity")] 
    public double? Rarity { get; set; }
}