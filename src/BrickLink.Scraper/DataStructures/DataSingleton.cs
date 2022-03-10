using BrickLink.Scraper.Model;

namespace BrickLink.Scraper.DataStructures;

public class DataSingleton
{
    public List<ItemData> ItemsData { get; set; }
    
    static DataSingleton() { }
    private DataSingleton()
    {
        ItemsData = new List<ItemData>();
    }

    public static DataSingleton Instance { get; } = new DataSingleton();
}