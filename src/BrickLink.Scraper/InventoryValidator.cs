using System.Text;
using BrickLink.Scraper.Model.XmlData;

namespace BrickLink.Scraper;

public class InventoryValidator
{
    public void Validate(XmlInventory inventory)
    {
        var errors = new List<ValidationError>();
        for (var index = 0; index < inventory?.Items?.Count; ++index)
        {
            var goodProps = new List<Tuple<string, string>>();
            var badProps = new List<string>();
            var item = inventory.Items[index];
            
            if (item.ItemId == null) badProps.Add("ItemId");
            else goodProps.Add(new Tuple<string, string>("ItemId", item.ItemId));
            
            if (item.Color == null) badProps.Add("Color");
            else goodProps.Add(new Tuple<string, string>("Color", item.Color));
            
            if (item.Condition == null) badProps.Add("Condition");
            else goodProps.Add(new Tuple<string, string>("Condition", item.Condition));
            
            if (item.MinQuantity == null) badProps.Add("MinQuantity");
            else goodProps.Add(new Tuple<string, string>("MinQuantity", item.MinQuantity));
            
            if (item.MaxPrice == null) badProps.Add("MaxPrice");
            else goodProps.Add(new Tuple<string, string>("MaxPrice", item.MaxPrice));

            if (badProps.Any()) errors.Add(new ValidationError { Index = index, GoodProps = goodProps, BadProps = badProps });
        }

        if (errors.Any())
        {
            var result = errors.Select(error => error.BuildErrorString()).ToArray();
            var mergeResult = string.Join("", result);
            throw new NullReferenceException(mergeResult);
        }
    }
}

public class ValidationError
{
    public ValidationError()
    {
        GoodProps = new List<Tuple<string, string>>();
        BadProps = new List<string>();
    }
    
    public List<Tuple<string, string>> GoodProps;
    public List<string> BadProps;
    public int Index { get; set; }

    public string BuildErrorString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append($"Item {Index} had empty or missing properties ");
        
        BadProps.ForEach(badprop => builder.Append($"{badprop}, "));

        if (GoodProps.Any())
            builder.Append("with values ");
        
        GoodProps.ForEach(goodProp => builder.Append( $"{goodProp.Item1}={goodProp.Item2}, "));

        builder.AppendLine("check Inventory.txt for empty or missing properties.");

        return builder.ToString();
    }
}