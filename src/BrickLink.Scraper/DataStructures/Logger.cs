namespace BrickLink.Scraper.DataStructures;

public static class Logger
{
    private static readonly StreamWriter Writer;
    private static readonly StreamWriter ErrorWriter;

    static Logger()
    {
        var root = Environment.CurrentDirectory;
        var filePath = Path.Combine(root, Constants.LogDirectory);
        
        var file = Path.Combine(filePath, Constants.LogFile);
        Writer = File.AppendText(file);

        var errorFile = Path.Combine(filePath, Constants.ErrorLogFile);
        ErrorWriter = File.AppendText(errorFile);
    }

    public static void Log(string? message)
    {
        Writer.Write("Log Entry : ");
        Writer.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
        Writer.WriteLine($"{message}");
    }
    
    public static void Log(Exception exception)
    {
        ErrorWriter.Write("Error Entry : ");
        ErrorWriter.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
        ErrorWriter.WriteLine($"{exception.Message}");
        ErrorWriter.WriteLine($"{exception.StackTrace}");
    }
    
    public static void EndLog()
    {
        Writer.Flush();
        Writer.Dispose();
        ErrorWriter.Flush();
        ErrorWriter.Dispose();
    }
}