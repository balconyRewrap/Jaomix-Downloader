using HtmlAgilityPack;

namespace Jaomix_Parser;

public class TitleMaker
{
    public static string Maker(string url)
    {
        var web = new HtmlWeb();
        var document = web.Load(url);
        // xPath элемента с главой
        string xTitle = "//*[@id=\"dfklje\"]";
        string title;
        int error = 0;
        try
        {
            // Мог бы использовать title из Initialization, но он иногда каким-то образом их другого потока получает title и получается мешанина в книге
            title = document.DocumentNode.SelectSingleNode(xTitle).InnerText;
        }
        catch (Exception)

        {

            while (true)
            {
                document = web.Load(url);
                Thread.Sleep(4000);
                if (document.DocumentNode.SelectSingleNode(xTitle) != null)
                {
                    title = document.DocumentNode.SelectSingleNode(xTitle).InnerText;
                    break;
                }

                error++;
                Console.WriteLine($"Количество перезагрузок страницы - {error}, url страницы - {url}");
            }
        }
        return title;
    }
}