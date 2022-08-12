using System.Text;
using HtmlAgilityPack;

namespace Jaomix_Parser;

public class SlowBookDownloader
{
    public void MakeBook(string folder, string[] links, string bookFileTxt, string bookFileEpub, string bookName, string authorName, int dec, FileMaker fileMaker, string finalFileType)
    {
        foreach (string link in links)
        {
            fileMaker.Maker(DownloadChapter(link), bookFileTxt, bookName, authorName, dec);
            Console.WriteLine(dec);
            dec++;
        }
        if (finalFileType != "1")
        {
            TxtToEpubConverter.Converter(bookFileTxt, bookFileEpub);

        }
    }

    private string DownloadChapter(string url)
    {
        int countP = 1;
        string element = "/html/body/div[1]/div[3]/div/div/div/div/article/div/div[2]/div/p[" +
                         Convert.ToString(countP) + "]";
        string text = "";
        var web = new HtmlWeb();
        var document = web.Load(url);
        // xPath элемента с главой
        string xTitle = "//*[@id=\"dfklje\"]";
        string title = document.DocumentNode.SelectSingleNode(xTitle).InnerText;
        // так pandoc (конвертер) распознает название главы
        text += "\n# " + title + "\n \n \n";
        var x = document.DocumentNode.SelectSingleNode(element);
        string? paragraph = x.InnerText;
        while (true)
        {
            x = document.DocumentNode.SelectSingleNode(element);
            if (x != null)
            {
                paragraph = x.InnerText;
                bool uselessParChecker = paragraph.Contains("Jaomix") ||
                                         paragraph.Contains("Ранобэ, Новеллы на русском, читать онлайн,") ||
                                         paragraph.Contains("Переводчик") ||
                                         paragraph.Contains("Если вы обнаружите какие-либо ошибки");
                // epub необходимо два символа перехода для нового параграфа
                if (!uselessParChecker) text += paragraph + "\n \n";


                countP++;
                element = "/html/body/div[1]/div[3]/div/div/div/div/article/div/div[2]/div/p[" +
                          Convert.ToString(countP) + "]";
            }
            else
            {
                break;
            }
        }

        try
        {
            text = HtmlEntity.DeEntitize(text);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return text;
    }
}