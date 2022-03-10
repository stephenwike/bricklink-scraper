// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using BrickLink.Scraper;
using BrickLink.Scraper.DataStructures;
using BrickLink.Scraper.Helpers;

// Deserialize XML
var deserializer = new XmlDeserializer();
deserializer.DeserializeConfiguration();
var inventory = deserializer.DeserializeInventory();
if (inventory == null) throw new Exception("Inventory cannot be null");
new InventoryValidator().Validate(inventory);

// Create ItemData
var dataManager = new DataManager();
dataManager.CreateItemData(inventory.Items);

// Store web results for duplicates and repeat item retrieval.
var stopwatch = new Stopwatch();
stopwatch.Start();

var fileStore = new PartFileStore();
var files = fileStore.SaveFiles(inventory.Items);
fileStore.Dispose();

stopwatch.Stop();
var elapsedTime = stopwatch.ElapsedMilliseconds;

// Iterate through each file.
stopwatch = new Stopwatch();
stopwatch.Start();

for (var index = 0; index < files.Count; ++index)
{
    var scrapeData = new WebScraper().FileScrape(files[index]);

    if (scrapeData == null || !scrapeData.Any())
        throw new Exception($"Scrape returned no data for file, \"{files[index]}\".");

    DataSingleton.Instance.ItemsData[index].Rarity = ValueHelper.GetRarityRatio(scrapeData.Count);

    dataManager.MapScrapeData(scrapeData, index);
}

stopwatch.Stop();
var elapsedTime2 = stopwatch.ElapsedMilliseconds;

// Weight Store Results
var stores = new StoreWeigher().Weigh();

// Build CSV
var csvBuilder = new CsvBuilder();
var table = csvBuilder.Build(stores);

// Print CSV
var csvPrinter = new CsvPrinter();
csvPrinter.Print(table);
