namespace BrickLink.Scraper.Entities;

public class SellerItemEntity
{
    public SellerItemEntity(string? name)
    {
        Name = name ?? throw new Exception("SellerItemEntity.constructor, name cannot be null.");
    }
    
    public string Name { get; set; }
    public string? Price { get; set; }
    public string? Quantity { get; set; }
    
}