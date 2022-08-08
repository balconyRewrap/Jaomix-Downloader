using System.Text;
using HtmlAgilityPack;

namespace Jaomix_Parser;

internal class MultiThreadingChapterDownloader
{
    public void Initializer(string folder)
    {
        Console.WriteLine("Введите название файла, куда запишется книга (c .txt)");
        string bookFile = folder + Console.ReadLine();
        Console.WriteLine(
            "Введите название файла, где находятся ссылки (с .txt) \n Он должен находиться в папке, которую вы задали в начале");
        string linksFile = folder + Console.ReadLine();
        Console.WriteLine("Введите название книги");
        string bookName = Console.ReadLine();
        Console.WriteLine("Введите имя автора");
        string authorName = Console.ReadLine();
        var chaptersList = new Dictionary<string, string>();

        string[] links = File.ReadAllLines(linksFile, Encoding.UTF8);
        int dec = 1;
        var fileMaker = new FileMaker();
        string title;
        Parallel.ForEach(links, link =>
        {
            title = Title.TitleMaker(link);
            string chapterFile = Title.TitleNormalizer(title) + ".txt";
            chaptersList.Add(link, chapterFile);
            fileMaker.ParrallelMaker(Downloader(link, title), folder + chapterFile);
        });
        foreach (string link in links)
        {
            string text = File.ReadAllText(folder + chaptersList[link]);
            fileMaker.Maker(text, bookFile, bookName, authorName, dec);
            dec++;
        }
    }

    private string Downloader(string url, string title)
    {
        int countP = 1;
        string element = "/html/body/div[1]/div[3]/div/div/div/div/article/div/div[2]/div/p[" +
                         Convert.ToString(countP) + "]";
        string text = "";
        var web = new HtmlWeb();
        var document = web.Load(url);

        text += "\n# " + title + "\n \n \n";
        var x = document.DocumentNode.SelectSingleNode(element);

        while (true)
        {
            x = document.DocumentNode.SelectSingleNode(element);
            if (x != null)
            {
                string paragraph = x.InnerText;
                bool uselessBoolPar = paragraph.Contains("Jaomix") ||
                                      paragraph.Contains("Ранобэ, Новеллы на русском, читать онлайн,") ||
                                      paragraph.Contains("Переводчик") ||
                                      paragraph.Contains("Если вы обнаружите какие-либо ошибки");

                if (!uselessBoolPar) text += paragraph + "\n \n";


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