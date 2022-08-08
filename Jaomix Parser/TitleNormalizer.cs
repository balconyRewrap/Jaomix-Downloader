using HtmlAgilityPack;

namespace Jaomix_Parser;

internal class Title
{
    public static string TitleNormalizer(string title)
    {
        char[] charsForTrim = { '\\', '|', '/', ':', '*', '?', '"', '<', '>' };
        title = title.Trim(charsForTrim);
        return title;
    }

    public static string TitleMaker(string url)
    {
        var web = new HtmlWeb();
        var document = web.Load(url);
        string xTitle = "//*[@id=\"dfklje\"]";
        string title = document.DocumentNode.SelectSingleNode(xTitle).InnerText;
        return title;
    }
}