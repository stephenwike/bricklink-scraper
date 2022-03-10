using BrickLink.Scraper.DataStructures;
using BrickLink.Scraper.Helpers;
using BrickLink.Scraper.Model;

namespace BrickLink.Scraper;

public class WeightsCalculator
{
    public double CalculateUniqueItemScore(int uniqueItemsCount)
    {
        var numItems = DataSingleton.Instance.ItemsData.Count;
        return ((double)uniqueItemsCount / (double)numItems) * 100;
    }

    public double? CalculateTotalQuantityScore_QOnly(KeyValuePair<string, SellerRanking> seller)
    {
        var score = DataSingleton.Instance.ItemsData
            .Where(itemData => itemData?.SellerItems != null && itemData.SellerItems.ContainsKey(seller.Key))
            .Sum(item =>
            {
                if (item?.SellerItems == null)
                    throw new Exception("WeightsCalculator.CalculateTotalQuantityScore, item.SellerItems cannot be null");
                double needed = item.QuantityNeeded ?? 0;
                double has = ValueHelper.ParseQuantity(item.SellerItems[seller.Key].Quantity);

                if (has >= needed) 
                    return 100;
                return (has / needed) * 100.0;
            });
        return score / seller.Value.UniqueItems.Count;
    }
    
    public double? CalculateTotalQuantityScore(KeyValuePair<string, SellerRanking> seller)
    {
        var score = DataSingleton.Instance.ItemsData
            .Where(itemData => itemData?.SellerItems != null && itemData.SellerItems.ContainsKey(seller.Key))
            .Sum(item =>
            {
                if (item?.SellerItems == null)
                    throw new Exception("WeightsCalculator.CalculateTotalQuantityScore, item.SellerItems cannot be null");
                double needed = item.QuantityNeeded ?? 0;
                double has = ValueHelper.ParseQuantity(item.SellerItems[seller.Key].Quantity);

                if (has >= needed) 
                    return 100;
                return (has / needed) * 100.0;
            });
        return score / DataSingleton.Instance.ItemsData.Count;
    }
    
    public double? CalculatePricePointScore(double? affordability)
    {
        if (affordability == null) throw new Exception("WeightsCalculator.CalculatePricePointScore, affordability cannot be null,");
        if (affordability >= 2.5) return 0;
        if (affordability <= 0.5) return 100;

        // Eq: log1.0137(3.2-x)+27
        var upper = 3.2 - affordability ?? 2.5;
        var lower = 1.0137;
        var logUpper = Math.Log(upper);
        var logLower = Math.Log(lower);
        var result = (logUpper / logLower) + 27;
        return result;
    }

    public double? CalculateSellerPricePointScore(SellerRanking sellerValue)
    {
        double? averageScore = 2.5;
        if (sellerValue?.Affordability != null && sellerValue?.UniqueItems != null)
            averageScore = sellerValue.Affordability / (double)sellerValue.UniqueItems.Count;

        var result = CalculatePricePointScore(averageScore);
        return result;
    }

    public double? CalculatePartRarityScore(KeyValuePair<string, SellerRanking> seller)
    {
        var score = DataSingleton.Instance.ItemsData
            .Where(itemData => itemData?.SellerItems != null && itemData.SellerItems.ContainsKey(seller.Key))
            .Sum(item => item.Rarity);

        return score;
    }
}