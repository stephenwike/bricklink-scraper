using BrickLink.Scraper.DataStructures;
using BrickLink.Scraper.Entities;
using BrickLink.Scraper.Helpers;
using BrickLink.Scraper.Model.TableData;

namespace BrickLink.Scraper;

public class CsvBuilder
{
    public TableData Build(List<string> sellers)
    {
        var table = new TableData();
        table.Sellers = sellers;
        
        DataSingleton.Instance.ItemsData.ForEach(item =>
        {
            // Get Static Row Data
            var part = new PartRow();
            part.Color = TextHelper.GetColor(item.Color);
            part.Description = TextHelper.GetDescription(item.Description ?? string.Empty);
            part.PartId = item.PartId;
            part.QuantityNeeded = item.QuantityNeeded ?? 0;
            
            // Get Dynamic Row Data
            sellers.ForEach(seller =>
            {
                var id = TextHelper.GetPartId(item.PartId, item.Color);
                if (item?.SellerItems != null && item.SellerItems.ContainsKey(seller))
                {
                    var sellerItem = item.SellerItems[seller];
                    part?.Sellers?.Add(sellerItem);
                    if (part?.QuantityFound != null) 
                        part.QuantityFound += ValueHelper.ParseQuantity(sellerItem.Quantity);
                }
                else
                {
                    part.Sellers.Add(new SellerItemEntity("none")
                    {
                        Price = "",
                        Quantity = "",
                    });
                }
            });
            
            table.Parts.Add(part);
        });

        return table;
    }
}