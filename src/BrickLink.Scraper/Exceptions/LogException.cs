using BrickLink.Scraper.DataStructures;

namespace BrickLink.Scraper.Exceptions;

public class LogException : IOException
{
    public LogException(string message) : base(message)
    {
        Logger.Log(message);
    }
}