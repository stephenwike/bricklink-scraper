using System.Xml.Serialization;
using BrickLink.Scraper.Model;
using BrickLink.Scraper.Model.XmlData;

namespace BrickLink.Scraper;

public class XmlDeserializer
{
    public XmlInventory DeserializeInventory()
    {
        
        
        XmlSerializer serializer = new XmlSerializer(typeof(XmlInventory));
        var currentDirectory = Environment.CurrentDirectory;
        var directory = Path.Combine(currentDirectory, Constants.InputsDirectory);
        var filePath = Path.Combine(directory, Configuration.InventoryFile);

        if (!File.Exists(filePath))
            throw new Exception($"Could not find file {Configuration.InventoryFile} in location {directory}.");

        XmlInventory? inventory = null;
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open)) 
        {
            inventory = serializer.Deserialize(fileStream) as XmlInventory;
        }

        if (inventory?.Items == null)
            throw new Exception($"Inventory is empty.  Make sure XML_Inventory is populated and correctly formatted.");
        
        if (!inventory.Items.Any())
            throw new Exception($"Inventory is empty.  Make sure XML_Inventory is populated.");

        return inventory;
    }
    
    public void DeserializeConfiguration()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(XmlConfiguration));
        var filepath = Path.Combine(Environment.CurrentDirectory, Constants.InputsDirectory, Constants.XmlConfigurationFile);

        if (!File.Exists(filepath))
            return; // If there is no configuration.txt, configurations have defaults.

        XmlConfiguration? xmlConfig = null;
        using (FileStream fileStream = new FileStream(filepath, FileMode.Open)) 
        {
            xmlConfig = serializer.Deserialize(fileStream) as XmlConfiguration;
        }

        if (xmlConfig != null)
        {
            Configuration.InventoryFile = xmlConfig?.InventoryFile ?? "Inventory.xml";
            Configuration.CacheTimeHoursPolicy = xmlConfig?.CacheTimeHoursPolicy ?? 24;
            Configuration.HtmlLoadTimeMs = xmlConfig?.HtmlLoadTimeMs ?? 250;
            Configuration.ResultsPerPage = xmlConfig?.ResultsPerPage ?? 100;
            Configuration.SortPartBy = xmlConfig?.SortPartsBy ?? 1;
            Configuration.MinQuantityPercent = xmlConfig?.MinQuantityPercent ?? 10;
            Configuration.PreferredSellers = xmlConfig?.PreferredSellers?.Seller ?? new List<string>();
            Configuration.IgnoredSellers = xmlConfig?.IgnoredSellers?.Seller ?? new List<string>();
            Configuration.SellerWeights = xmlConfig?.SellerWeights ?? new XmlSellerWeights
                { PricePoint = 1.2, Quantity = 1.0, Rarity = 1.5 };
            Configuration.NumberOfSellers = xmlConfig?.NumberOfSellers ?? 40;
        }
    }
}