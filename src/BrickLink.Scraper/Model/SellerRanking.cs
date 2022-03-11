using BrickLink.Scraper.Exceptions;

namespace BrickLink.Scraper.Model;

public class SellerRanking
{
    public SellerRanking(string id)
    {
        Id = id ?? throw new LogException("SellerRanking.constructor cannot be null");
        UniqueItems = new HashSet<string>();
    }
    
    public string Id { get; set; }
    public HashSet<string> UniqueItems { get; set; }

    public string? Quantity { get; set; }
    public double? Affordability { get; set; }
}