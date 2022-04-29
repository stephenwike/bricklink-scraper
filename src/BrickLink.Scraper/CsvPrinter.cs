using System.Globalization;
using System.Text;
using BrickLink.Scraper.Exceptions;
using BrickLink.Scraper.Helpers;
using BrickLink.Scraper.Model.TableData;

namespace BrickLink.Scraper;

public class CsvPrinter
{
    private readonly StringBuilder _builder;

    public CsvPrinter()
    {
        _builder = new StringBuilder();
    }
    
    public void Print(TableData table)
    {
        PrintHeader(table.Sellers);
        PrintSecondaryHeader(table.Sellers.Count);
        PrintData(table.Parts);
        
        var root = Environment.CurrentDirectory;
        var outputPath = Path.Combine(root, Constants.OutputsDirectory);
        if (!Directory.Exists(Path.Combine(outputPath)))
            Directory.CreateDirectory(outputPath);
        var filePath = Path.Combine(outputPath, Constants.OutputFile);
        
        try
        {
            File.WriteAllText(filePath, _builder.ToString());
        }
        catch (Exception)
        {
            throw new LogException($"Failed to write CSV document.  Make sure you don't have {Constants.OutputFile} open in directory {outputPath}.");
        }
    }

    private void PrintData(List<PartRow> parts)
    {
        parts.ForEach(part =>
        {
            var staticRow = $"{part.PartId},{part.Color},{part.Description},{part.QuantityNeeded},{part.QuantityFound},{part.MaxPrice}";
            _builder.Append(staticRow);
            
            part.Sellers.ForEach(seller =>
            {
                string price = string.IsNullOrWhiteSpace(seller.Price) ? "" : ValueHelper.ParsePrice(seller.Price).ToString(CultureInfo.InvariantCulture);
                string quantity = string.IsNullOrWhiteSpace(seller.Quantity) ? "" : ValueHelper.ParseQuantity(seller.Quantity).ToString(CultureInfo.InvariantCulture);
                _builder.Append($",{quantity},{price}");
            });
                
            _builder.AppendLine();
        });
    }

    private void PrintSecondaryHeader(int count)
    {
        var headerString = $",,,,,";
        _builder.Append(headerString);
        
        for (var i = 0; i < count; ++i)
        {
            _builder.Append($",quantity,price");
        }
        _builder.AppendLine();
    }

    private void PrintHeader(List<string> sellers)
    {
        var headerString = $"P Id,Color,Desc,Need,Found,Max $";
        _builder.Append(headerString);
        
        sellers.ForEach(seller =>
        {
            _builder.Append($",{seller},");
        });
        _builder.AppendLine();
    }
}