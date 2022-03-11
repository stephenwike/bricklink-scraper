using System.Web;
using BrickLink.Scraper.Exceptions;

namespace BrickLink.Scraper.Helpers;

public class TextHelper
{
    public static string GetPartId(string? partId, string? color)
    {
        if (string.IsNullOrWhiteSpace(partId))
            throw new LogException("ValueHelper.GetPartId, partId cannot be null or whitespace.");
        if (string.IsNullOrWhiteSpace(color))
            throw new LogException("ValueHelper.GetPartId, color cannot be null or whitespace.");
        return $"{partId}_{color}";
    }
    
    public static string? HtmlDecode(string text)
    {
        if (text.IndexOf('\u2013') > -1) text = text.Replace('\u2013', '-'); // en dash
        if (text.IndexOf('\u2014') > -1) text = text.Replace('\u2014', '-'); // em dash
        if (text.IndexOf('\u2015') > -1) text = text.Replace('\u2015', '-'); // horizontal bar
        if (text.IndexOf('\u2017') > -1) text = text.Replace('\u2017', '_'); // double low line
        if (text.IndexOf('\u2018') > -1) text = text.Replace('\u2018', '\''); // left single quotation mark
        if (text.IndexOf('\u2019') > -1) text = text.Replace('\u2019', '\''); // right single quotation mark
        if (text.IndexOf('\u201a') > -1) text = text.Replace('\u201a', ','); // single low-9 quotation mark
        if (text.IndexOf('\u201b') > -1) text = text.Replace('\u201b', '\''); // single high-reversed-9 quotation mark
        if (text.IndexOf('\u201c') > -1) text = text.Replace('\u201c', '\"'); // left double quotation mark
        if (text.IndexOf('\u201d') > -1) text = text.Replace('\u201d', '\"'); // right double quotation mark
        if (text.IndexOf('\u201e') > -1) text = text.Replace('\u201e', '\"'); // double low-9 quotation mark
        if (text.IndexOf('\u2026') > -1) text = text.Replace("\u2026", "..."); // horizontal ellipsis
        if (text.IndexOf('\u2032') > -1) text = text.Replace('\u2032', '\''); // prime
        if (text.IndexOf('\u2033') > -1) text = text.Replace('\u2033', '\"'); // double prime
        var httpDecoded = HttpUtility.HtmlDecode(text);
        return httpDecoded;
    }

    public static string? GetDescription(string desc)
    {
        return desc.Replace(',', '.'); // no commas
    }

    public static string? GetColor(string? color)
    {
        return color switch
        {
            "1" => "White",
            "10" => "Dark Gray",
            "100" => "Glitter Trans-Dark Pink",
            "101" => "Glitter Trans-Clear",
            "102" => "Glitter Trans-Purple",
            "103" => "Bright Light Yellow",
            "104" => "Bright Pink",
            "105" => "Bright Light Blue",
            "106" => "Fabuland Brown",
            "107" => "Trans-Pink",
            "108" => "Trans-Bright Green",
            "109" => "Dark Blue-Violet",
            "11" => "Black",
            "110" => "Bright Light Orange",
            "111" => "Speckle Black-Silver",
            "113" => "Trans-Aqua",
            "114" => "Trans-Light Purple",
            "115" => "Pearl Gold",
            "116" => "Speckle Black-Copper",
            "117" => "Speckle DBGray-Silver",
            "118" => "Glow In Dark Trans",
            "119" => "Pearl Very Light Gray",
            "12" => "Trans-Clear",
            "120" => "Dark Brown",
            "121" => "Trans-Neon Yellow",
            "122" => "Chrome Black",
            "123" => "Mx White",
            "124" => "Mx Light Bluish Gray",
            "125" => "Mx Light Gray",
            "126" => "Mx Charcoal Gray",
            "127" => "Mx Tile Gray",
            "128" => "Mx Black",
            "129" => "Mx Red",
            "13" => "Trans-Black",
            "130" => "Mx Pink Red",
            "131" => "Mx Tile Brown",
            "132" => "Mx Brown",
            "133" => "Mx Buff",
            "134" => "Mx Terracotta",
            "135" => "Mx Orange",
            "136" => "Mx Light Orange",
            "137" => "Mx Light Yellow",
            "138" => "Mx Ochre Yellow",
            "139" => "Mx Lemon",
            "14" => "Trans-Dark Blue",
            "140" => "Mx Olive Green",
            "141" => "Mx Pastel Green",
            "142" => "Mx Aqua Green",
            "143" => "Mx Tile Blue",
            "144" => "Mx Medium Blue",
            "145" => "Mx Pastel Blue",
            "146" => "Mx Teal Blue",
            "147" => "Mx Violet",
            "148" => "Mx Pink",
            "149" => "Mx Clear",
            "15" => "Trans-Light Blue",
            "150" => "Medium Nougat",
            "151" => "Speckle Black-Gold",
            "152" => "Light Aqua",
            "153" => "Dark Azure",
            "154" => "Lavender",
            "155" => "Olive Green",
            "156" => "Medium Azure",
            "157" => "Medium Lavender",
            "158" => "Yellowish Green",
            "159" => "Glow In Dark White",
            "16" => "Trans-Neon Green",
            "160" => "Fabuland Orange",
            "161" => "Dark Yellow",
            "162" => "Glitter Trans-Light Blue",
            "163" => "Glitter Trans-Neon Green",
            "164" => "Trans-Light Orange",
            "165" => "Neon Orange",
            "166" => "Neon Green",
            "17" => "Trans-Red",
            "18" => "Trans-Neon Orange",
            "19" => "Trans-Yellow",
            "2" => "Tan",
            "20" => "Trans-Green",
            "21" => "Chrome Gold",
            "210" => "Mx Foil Dark Gray",
            "211" => "Mx Foil Light Gray",
            "212" => "Mx Foil Dark Green",
            "213" => "Mx Foil Light Green",
            "214" => "Mx Foil Dark Blue",
            "215" => "Mx Foil Light Blue",
            "216" => "Mx Foil Violet",
            "217" => "Mx Foil Red",
            "218" => "Mx Foil Yellow",
            "219" => "Mx Foil Orange",
            "22" => "Chrome Silver",
            "220" => "Coral",
            "221" => "Trans-Light Green",
            "222" => "Glitter Trans-Orange",
            "223" => "Satin Trans-Light Blue",
            "224" => "Satin Trans-Dark Pink",
            "225" => "Dark Nougat",
            "226" => "Trans-Light Bright Green",
            "227" => "Clikits Lavender",
            "228" => "Satin White",
            "229" => "Satin Trans-Black",
            "23" => "Pink",
            "230" => "Satin Trans-Purple",
            "231" => "Dark Salmon",
            "232" => "Satin Trans-Dark Blue",
            "233" => "Satin Trans-Bright Green",
            "234" => "Trans-Medium Purple",
            "235" => "Reddish Gold",
            "236" => "Neon Yellow",
            "237" => "Bionicle Copper",
            "238" => "Bionicle Gold",
            "239" => "Bionicle Silver",
            "24" => "Purple",
            "240" => "Medium Brown",
            "25" => "Salmon",
            "26" => "Light Salmon",
            "27" => "Rust",
            "28" => "Nougat",
            "29" => "Earth Orange",
            "3" => "Yellow",
            "31" => "Medium Orange",
            "32" => "Light Orange",
            "33" => "Light Yellow",
            "34" => "Lime",
            "35" => "Light Lime",
            "36" => "Bright Green",
            "37" => "Medium Green",
            "38" => "Light Green",
            "39" => "Dark Turquoise",
            "4" => "Orange",
            "40" => "Light Turquoise",
            "41" => "Aqua",
            "42" => "Medium Blue",
            "43" => "Violet",
            "44" => "Light Violet",
            "46" => "Glow In Dark Opaque",
            "47" => "Dark Pink",
            "48" => "Sand Green",
            "49" => "Very Light Gray",
            "5" => "Red",
            "50" => "Trans-Dark Pink",
            "51" => "Trans-Purple",
            "52" => "Chrome Blue",
            "54" => "Sand Purple",
            "55" => "Sand Blue",
            "56" => "Light Pink",
            "57" => "Chrome Antique Brass",
            "58" => "Sand Red",
            "59" => "Dark Red",
            "6" => "Green",
            "60" => "Milky White",
            "61" => "Pearl Light Gold",
            "62" => "Light Blue",
            "63" => "Dark Blue",
            "64" => "Chrome Green",
            "65" => "Metallic Gold",
            "66" => "Pearl Light Gray",
            "67" => "Metallic Silver",
            "68" => "Dark Orange",
            "69" => "Dark Tan",
            "7" => "Blue",
            "70" => "Metallic Green",
            "71" => "Magenta",
            "72" => "Maersk Blue",
            "73" => "Medium Violet",
            "74" => "Trans-Medium Blue",
            "76" => "Medium Lime",
            "77" => "Pearl Dark Gray",
            "78" => "Metal Blue",
            "8" => "Brown",
            "80" => "Dark Green",
            "81" => "Flat Dark Gold",
            "82" => "Chrome Pink",
            "83" => "Pearl White",
            "84" => "Copper",
            "85" => "Dark Bluish Gray",
            "86" => "Light Bluish Gray",
            "87" => "Sky Blue",
            "88" => "Reddish Brown",
            "89" => "Dark Purple",
            "9" => "Light Gray",
            "90" => "Light Nougat",
            "91" => "Light Brown",
            "93" => "Light Purple",
            "94" => "Medium Dark Pink",
            "95" => "Flat Silver",
            "96" => "Very Light Orange",
            "97" => "Blue-Violet",
            "98" => "Trans-Orange",
            "99" => "Very Light Bluish Gray",
            _ => "Unknown"
        };
    }
}