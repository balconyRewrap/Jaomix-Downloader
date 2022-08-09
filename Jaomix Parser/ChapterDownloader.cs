using System.Text;
using HtmlAgilityPack;

namespace Jaomix_Parser;

internal class ChapterDownloader
{
    public void Initialization(string folder)
    {
        Console.WriteLine("Введите название файла, куда запишется книга (без .txt)");
        string bookFile = Console.ReadLine();
        string bookFileTxt = bookFile + ".txt";
        string bookFileEpub = folder + bookFile+ ".epub";
        Console.WriteLine(
            "Введите название файла, где находятся ссылки (с .txt) \n Он должен находиться в папке, которую вы задали в начале");
        string linksFile = folder + Console.ReadLine();
        Console.WriteLine("Введите название книги");
        string bookName = Console.ReadLine();
        Console.WriteLine("Введите имя автора");
        string authorName = Console.ReadLine();
        string[] links = File.ReadAllLines(linksFile, Encoding.UTF8);
        int dec = 1;
        FileMaker fileMaker = new FileMaker();
        Starter(folder,links,bookFileTxt,bookFileEpub,bookName,authorName,dec,fileMaker);


    }
    private void Starter(string folder, string[] links, string bookFileTxt, string bookFileEpub, string bookName, string authorName, int dec, FileMaker fileMaker)
    {
        foreach (string link in links)
        {
            fileMaker.Maker(Downloader(link), folder + bookFileTxt, bookName, authorName, dec);
            Console.WriteLine(dec);
            dec++;
        }
        TxtToEpubConverter.Converter(folder+bookFileTxt, bookFileEpub);
    }

    private string Downloader(string url)
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