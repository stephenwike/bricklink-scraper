using BrickLink.Scraper.DataStructures;
using BrickLink.Scraper.Helpers;
using BrickLink.Scraper.Model;

namespace BrickLink.Scraper;

public class StoreWeigher
{
    public List<string> Weigh()
    {
        var rankings = CreateSellerRankings();
        var sellers = FilterSellersByRanking(rankings);

        return sellers.Take(Configuration.NumberOfSellers).ToList();
    }

    private List<string> FilterSellersByRanking(Dictionary<string, SellerRanking> rankings)
    {
        var rankingsList = rankings.ToList();
        var weightCalculator = new WeightsCalculator();
        var scores = rankingsList.Select(seller =>
        {
            if (seller.Value?.UniqueItems == null)
                throw new Exception("StoreWeigher.FilterSellersByRanking, UniqueItems should not be null");
            
            return new SellerScore(seller.Key)
            {
                QuantityScore = weightCalculator.CalculateTotalQuantityScore(seller),
                
                PricePointScore = weightCalculator.CalculateSellerPricePointScore(seller.Value),
                
                RarityScore = weightCalculator.CalculatePartRarityScore(seller),
            };
        }).ToList();
        
        var sortedByScore = scores.OrderByDescending(score => score.TotalScore).ToList();
        var sortedByPreferredSellers = sortedByScore.OrderByDescending(score =>
            Configuration.PreferredSellers.Contains(score.Id)).ToList();
        var finalList = sortedByPreferredSellers.Select(x => x.Id).ToList();

        return finalList;
    }

    private Dictionary<string, SellerRanking> CreateSellerRankings()
    {
        var sellersRankings = new Dictionary<string, SellerRanking>();
        
        DataSingleton.Instance.ItemsData.ForEach(part =>
        {
            if (part.SellerItems == null)
                throw new Exception("StoreWeigher.CreateSellerRankings, ItemsData[x].SellerItems cannot be null.");

            part.SellerItems.Keys.ToList().ForEach(key =>
            {
                var uniqueId = TextHelper.GetPartId(part.PartId, part.Color);
                var quantity = part.SellerItems[key].Quantity;
                var price = part.SellerItems[key].Price;
                var maxPrice = part.MaxPrice;
                var affordability = ValueHelper.GetAffordabilityRatio(price, maxPrice, key);

                if (!Configuration.IgnoredSellers.Contains(key))
                {
                    if (sellersRankings.ContainsKey(key))
                    {
                        sellersRankings[key].Quantity += quantity;
                        sellersRankings[key].Affordability += affordability;
                        var uniqueItems = sellersRankings[key].UniqueItems;
                        if (!uniqueItems.Contains(uniqueId))
                            uniqueItems.Add(uniqueId);
                    }
                    else
                    {
                        var ranking = new SellerRanking(key)
                        {
                            Quantity = quantity ?? throw new Exception("StoreWeigher.CreateSellerRankings, quantity cannot be null."),
                            Affordability = affordability,
                        };
                        ranking.UniqueItems.Add(uniqueId);
                        sellersRankings.Add(key, ranking);
                    }
                }
            });
        });

        return sellersRankings;
    }
}