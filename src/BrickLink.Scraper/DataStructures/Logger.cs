namespace BrickLink.Scraper.DataStructures;

public static class Logger
{
    private static readonly StreamWriter Writer;
    private static readonly StreamWriter ErrorWriter;

    static Logger()
    {
        var root = Environment.CurrentDirectory;
        var filePath = Path.Combine(root, Constants.LogDirectory);
        Directory.CreateDirectory(filePath);

        var file = Path.Combine(filePath, Constants.LogFile);
        if (!File.Exists(file)) File.CreateText(file);
        Writer = File.AppendText(file);

        var errorFile = Path.Combine(filePath, Constants.ErrorLogFile);
        if (!File.Exists(errorFile)) File.CreateText(errorFile);
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