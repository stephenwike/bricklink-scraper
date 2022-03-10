using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using BrickLink.Scraper.Model;

namespace BrickLink.Scraper.Helpers;

public static class ValueHelper
{
    public static int ParseQuantity(string? quantity)
    {
        if (string.IsNullOrWhiteSpace(quantity))
            throw new Exception("ValueHelper.ParseQuantity, quantity cannot be null or whitespace.");
            
        var match = new Regex(@"(?<Quantity>[0-9]+)").Match(quantity);
        if (match.Success)
        {
            var value = match.Groups["Quantity"].Value;
            return int.Parse(value);
        }
            
        return 0;
    }

    public static double GetAffordabilityRatio(string? priceStr, string? maxPriceStr, string? name)
    {
        if (string.IsNullOrWhiteSpace(priceStr))
        {
            Console.WriteLine($"ValueHelper.GetPartId, priceStr cannot be null or whitespace for seller {name}.");
            return 0;
        }
        if (string.IsNullOrWhiteSpace(maxPriceStr))
        {
            Console.WriteLine($"ValueHelper.GetPartId, maxPriceStr cannot be null or whitespace for seller {name}.");
            return 0;
        }
        
        double price = 0;
        double maxPrice = 0;
        
        var priceMatch = new Regex(@"(?<Price>[0-9.]+)").Match(priceStr);
        if (priceMatch.Success)
        {
            var value = priceMatch.Groups["Price"].Value;
            price = double.Parse(value);
        }
        
        var maxPriceMatch = new Regex(@"(?<MaxPrice>[0-9.]+)").Match(maxPriceStr);
        if (maxPriceMatch.Success)
        {
            var value = maxPriceMatch.Groups["MaxPrice"].Value;
            maxPrice = double.Parse(value);
        }

        if (price >= maxPrice) return 2.5;
        return price / maxPrice * 2.5;
    }

    public static double ParsePrice(string? sellerItemPrice)
    {
        if (string.IsNullOrWhiteSpace(sellerItemPrice))
            throw new Exception("ValueHelper.ParsePrice, sellerItemPrice cannot be null or whitespace.");
        
        var match = new Regex(@"(?<Price>[0-9.]+)").Match(sellerItemPrice);
        if (match.Success)
        {
            var value = match.Groups["Price"].Value;
            return double.Parse(value);
        }
            
        return 0;
    }

    public static double GetRarityRatio(double itemCount)
    {
        return itemCount / 100.0;
    }

    public static int GetQuantityNeeded(string? minQuantity)
    {
        if (string.IsNullOrWhiteSpace(minQuantity))
            throw new Exception("ValueHelper.GetQuantityNeeded, itemMinQuantity cannot be null or whitespace.");
        
        var match = new Regex(@"(?<Price>[0-9]+$)").Match(minQuantity);
        if (match.Success)
        {
            var value = match.Groups["Price"].Value;
            return int.Parse(value);
        }

        return 0;
    }

    public static int GetMinQuantityFromPercent(string? minQuantity)
    {
        int minQ = GetQuantityNeeded(minQuantity);
        
        if (minQ > 100) // Can't be greater than 100%
            minQ = 100;
        if (minQ < 0) // Can't be less than 0%
            minQ = 0;

        double value = ((double)minQ * (double)Configuration.MinQuantityPercent) / 100.0;
        return (int)Math.Round(value);
    }

    
}