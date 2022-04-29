using BrickLink.Scraper.Entities;

namespace BrickLink.Scraper.Model.TableData;

public class PartRow
{
    public string? PartId { get; set; }
    public string? Color { get; set; }
    public string? Description { get; set; }
    public int QuantityNeeded { get; set; } = 0;
    public int QuantityFound { get; set; } = 0;
    public List<SellerItemEntity> Sellers { get; set; } = new List<SellerItemEntity>();
    public double MaxPrice { get; set; }
}