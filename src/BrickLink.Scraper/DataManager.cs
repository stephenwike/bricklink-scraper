using BrickLink.Scraper.DataStructures;
using BrickLink.Scraper.Entities;
using BrickLink.Scraper.Exceptions;
using BrickLink.Scraper.Helpers;
using BrickLink.Scraper.Model;
using BrickLink.Scraper.Model.XmlData;

namespace BrickLink.Scraper;

public class DataManager
{
    public void CreateItemData(List<XmlItem>? items)
    {
        if (items == null)
            throw new LogException("DataMapper.CreateItemData, List<XmlItems> cannot be null.");
        
        var itemsData = items.Select(item => new ItemData()
        {
            PartId = item.ItemId ?? throw new LogException("DataMapper.CreateItemData, ItemId cannot be null or empty."),
            Color = item.Color ?? throw new LogException("DataMapper.CreateItemData, Color cannot be null or empty."),
            MaxPrice = item.MaxPrice ?? throw new LogException("DataMapper.CreateItemData, MaxPrice cannot be null or empty."),
            QuantityNeeded = ValueHelper.GetQuantityNeeded(item.MinQuantity),
            Description = string.Empty,
        }).ToList();

        DataSingleton.Instance.ItemsData = itemsData;
    }

    public void MapScrapeData(List<ItemScrape> scrapeItemsData, int index)
    {
        var descriptionFilled = false;
        
        // Update seller information.
        var sellers = scrapeItemsData.Select(item =>
        {
            var sellerItem = new SellerItemEntity(item.Seller)
            {
                Price = item.Price,
                Quantity = item.Quantity,
            };

            // Fill with first description
            if (!descriptionFilled)
            {
                DataSingleton.Instance.ItemsData[index].Description = item.Description;
                descriptionFilled = true;
            }

            return sellerItem;
        }).GroupBy(x => x.Name).Select(x => x.First()).ToDictionary(entity => entity.Name, entity => entity);
        
        // Update part results.
        DataSingleton.Instance.ItemsData[index].SellerItems = sellers;
        DataSingleton.Instance.ItemsData[index].TotalViableProduct = sellers.Sum(x => ValueHelper.ParseQuantity(x.Value.Quantity));
    }
}