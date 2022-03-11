using BrickLink.Scraper.DataStructures;
using BrickLink.Scraper.Exceptions;
using BrickLink.Scraper.Model;
using BrickLink.Scraper.Model.XmlData;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BrickLink.Scraper;

public class PartFileStore : IDisposable
{
    private bool IsLoaded = false;
    private IWebDriver Driver => _lazyDriver.Value;
    private readonly Lazy<IWebDriver> _lazyDriver;
    
    public PartFileStore()
    {
        _lazyDriver = new Lazy<IWebDriver>(() =>
        {
            var chromeOptions = new ChromeOptions();
            if (Configuration.HeadlessBrowser)
                chromeOptions.AddArguments("headless");

            for (var index = 0; index < Constants.DriverVersions.Length; ++index)
            {
                try
                {
                    var path = Path.Combine(Environment.CurrentDirectory, Constants.DriversDirectory, Constants.DriverVersions[index]);
                    var chromeDriver = new ChromeDriver(path, chromeOptions);
                    IsLoaded = true;
                    return chromeDriver;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            throw new LogException("Chrome with supported version is not installed on this machine.");
        });
    }
    
    public List<string> SaveFiles(List<XmlItem>? items)
    {
        if (items == null)
            throw new LogException("DataMapper.CreateItemData, List<XmlItems> cannot be null.");
        
        // Create html files directory if needed.
        var path = Path.Combine(Environment.CurrentDirectory, Constants.HtmlFilesDirectory);
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        
        // Clean up out of date files
        var outdatedFiles = new DirectoryInfo(path).GetFiles("*.html")
            .Where(file => file.LastWriteTime < DateTime.Now.AddHours(-Configuration.CacheTimeHoursPolicy)).ToList();
        outdatedFiles.ForEach(file => file.Delete());

        // Get all html files
        var files = new DirectoryInfo(path).GetFiles("*.html").Select(file => file.Name).ToList();
        
        // Save all files not already saved within the policy creation date.
        return items!.Select(item =>
        {
            var filename = $"{item.ItemId} {item.Color} {item.Condition} {item.ItemType} {item.MaxPrice?.Replace('.', '_')}.html";

            if (item.ItemId != null && files.Contains(filename))
            {
                return files[files.IndexOf(filename)];
            }

            // Get URL.
            var urlBuilder = new UrlBuilder();
            var url = urlBuilder.Build(item);
            
            // Fetch HTML from URL with chromedriver.
            Driver.Navigate().GoToUrl(url);
            Thread.Sleep(Configuration.HtmlLoadTimeMs);
            var html = Driver.PageSource;
            
            // Write HTML to cache file.
            var outputPath = Path.Combine(path, filename);
            File.WriteAllText(outputPath, html);

            return filename;
        }).ToList();
    }
    
    public void Dispose()
    {
        if (IsLoaded) Driver.Dispose();
    }
}