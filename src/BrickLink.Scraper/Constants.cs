namespace BrickLink.Scraper;

public static class Constants
{
    public const string XmlConfigurationFile = "Configuration.xml";
    public const string HtmlFilesDirectory = "HTML_Files";
    public const string DriversDirectory = "drivers";
    public static readonly string[] DriverVersions = { "99.0.4844.35", "98.0.4758.102", "97.0.4692.71" };
    public const string InputsDirectory = "Inputs";
    public const string OutputsDirectory = "Outputs";
    public static string OutputFile = "Results.csv";
    public static string LogDirectory = "Log";
    public static string LogFile = "info.log";
    public static string ErrorLogFile = "error.log";
}