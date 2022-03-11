using BrickLink.Scraper.Exceptions;

namespace BrickLink.Scraper.Model;

public class SellerScore
{
    public SellerScore(string? id)
    {
        Id = id ?? throw new LogException("SellerScore.constructor, Id cannot be null.");
    }
    
    public string Id { get; set; }
    public double? QuantityScore { get; set; }
    public double? UniquePartsScore { get; set; }
    public double? PricePointScore { get; set; }
    public double? RarityScore { get; set; }
    
    public double TotalScore
    {
        get
        {
            double? score = 0;
            if (PricePointScore != null)
                score += PricePointScore * Configuration.SellerWeights.PricePoint;
            if (QuantityScore != null)
                score += QuantityScore * Configuration.SellerWeights.Quantity;
            if (RarityScore != null)
                score += RarityScore * Configuration.SellerWeights.Rarity;
            return score ?? 0;
        }
    }
}