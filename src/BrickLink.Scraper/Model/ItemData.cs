using BrickLink.Scraper.Entities;

namespace BrickLink.Scraper.Model;

public class ItemData
{
    public string? PartId { get; set; }
    public string? Color { get; set; }
    public string? Description { get; set; }
    public int? TotalViableProduct { get; set; }
    public string? MaxPrice { get; set; }
    public int? QuantityNeeded { get; set; }
    public Dictionary<string, SellerItemEntity>? SellerItems { get; set; }
    public double? Rarity { get; set; }
}