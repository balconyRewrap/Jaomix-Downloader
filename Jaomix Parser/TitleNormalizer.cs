using HtmlAgilityPack;

namespace Jaomix_Parser;

internal class Title
{
    public static string TitleNormalizer(string title)
    {
        // убираю из главы все символы, на которые будет ругаться винда в названиях файлов
        char[] charsForTrim = { '\\', '|', '/', ':', '*', '?', '"', '<', '>' };
        title = title.Trim(charsForTrim);
        return title;
    }

    public static string TitleMaker(string url)
    {
        var web = new HtmlWeb();
        var document = web.Load(url);
        // xPath элемента с главой
        string xTitle = "//*[@id=\"dfklje\"]";
        string title = document.DocumentNode.SelectSingleNode(xTitle).InnerText;
        return title;
    }
}