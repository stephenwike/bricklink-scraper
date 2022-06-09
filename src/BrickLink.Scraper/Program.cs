using System.Diagnostics;
using BrickLink.Scraper;
using BrickLink.Scraper.DataStructures;
using BrickLink.Scraper.Exceptions;
using BrickLink.Scraper.Helpers;
using System.Linq;

try
{
    // Deserialize XML
    var deserializer = new XmlDeserializer();
    deserializer.DeserializeConfiguration();
    var inventory = deserializer.DeserializeInventory() ?? throw new LogException("Inventory cannot be null");
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
    Logger.Log($"HTML docs collected in {elapsedTime} milliseconds.");

    // Iterate through each file.
    stopwatch = new Stopwatch();
    stopwatch.Start();

    for (var index = 0; index < files.Count; ++index)
    {
        var scrapeData = new WebScraper().FileScrape(files[index]);

        if (scrapeData == null || !scrapeData.Any())
        {
            Logger.Log($"Scrape returned no data for file, \"{files[index]}\".");
            continue;
        }

        DataSingleton.Instance.ItemsData[index].Rarity = ValueHelper.GetRarityRatio(scrapeData.Count);

        dataManager.MapScrapeData(scrapeData, index);
    }

    stopwatch.Stop();
    elapsedTime = stopwatch.ElapsedMilliseconds;
    Logger.Log($"Scrape data mapped in {elapsedTime} milliseconds.");
    
    // Weight Store Results
    stopwatch = new Stopwatch();
    stopwatch.Start();
    
    var stores = new StoreWeigher().Weigh();

    stopwatch.Stop();
    elapsedTime = stopwatch.ElapsedMilliseconds;
    Logger.Log($"Stores weighted in {elapsedTime} milliseconds.");
    
    // Build CSV
    var csvBuilder = new CsvBuilder();
    var table = csvBuilder.Build(stores);

    // Print CSV
    var csvPrinter = new CsvPrinter();
    csvPrinter.Print(table);

}
catch (LogException)
{
    // Nothing to do, message and stacktrace logged in LogException constructor.
}
catch (Exception e)
{
    Logger.Log(e);
}
finally
{
    Logger.EndLog();
}
