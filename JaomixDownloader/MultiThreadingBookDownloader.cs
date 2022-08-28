using HtmlAgilityPack;
using System.Globalization;

namespace JaomixDownloader;

public class MultiThreadingBookDownloader
{
    private readonly MuThDownloaderParameters _muThDownloaderBook;

    public MultiThreadingBookDownloader(MuThDownloaderParameters muThDownloaderParameters)
    {
        _muThDownloaderBook = muThDownloaderParameters;
    }

    public void MakeBook()
    {
        ParallelChaptersDownloader();

        MergeBookChapterFiles();

        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookTxtMakeSuccess", CultureInfo.CurrentCulture));

    }

    private void ParallelChaptersDownloader()
    {
        Parallel.ForEach(_muThDownloaderBook.ChaptersLinks, chapterLink =>
        {
            Console.WriteLine(chapterLink);
            string chapterFile = chapterLink.NormalizeUrl() + ".txt";
            string chapterRawText = "";
            string chapterText = "";


            // Регулярно вылетает ошибка, связанная с неправильной загрузкой страницы.
            // Решается простым перезапуском метода
            while (chapterRawText == "")
            {
                try
                {
                    chapterRawText = GiveChapterRawText(chapterLink);
                }
                catch (Exception e)
                {
                    // Console.WriteLine("2");
                    // Console.WriteLine(e);
                }
            }

            try
            {
                chapterText = GiveChapterNormalizedText(chapterRawText);
            }
            catch (Exception e)
            {
                Console.WriteLine("3");
                Console.WriteLine(e);
            }

            try
            {
                FileMaker.MakeChapterFile(chapterText, _muThDownloaderBook.Folder + chapterFile);
            }
            catch (Exception e)
            {
                Console.WriteLine("4");
                Console.WriteLine(e);
            }


        });
    }

    private string GiveChapterRawText(string url)
    {
        int countParagraph = 1;
        string paragraphElement = "/html/body/div[1]/div[3]/div/div/div/div/article/div/div[2]/div/p[" +
                         Convert.ToString(countParagraph) + "]";

        var web = new HtmlWeb();
        var htmlDocument = web.Load(url);
        JaomixMetadata titleGiver = new JaomixMetadata();
        string title = titleGiver.GiveChapterTitle(url);

        HtmlNode paragraphHtmlNode;
        // так pandoc (конвертер) распознает название главы
        string text = "\n# " + title + "\n \n \n";
        try
        {
            paragraphHtmlNode = htmlDocument.DocumentNode.SelectSingleNode(paragraphElement);
            while (true)
            {
                paragraphHtmlNode = htmlDocument.DocumentNode.SelectSingleNode(paragraphElement);
                if (paragraphHtmlNode != null)
                {
                    string paragraph = paragraphHtmlNode.InnerText;
                    // epub необходимо два символа перехода для нового параграфа
                    if (!paragraph.IsUselessParagraph()) text += paragraph + "\n \n";


                    countParagraph++;
                    paragraphElement = "/html/body/div[1]/div[3]/div/div/div/div/article/div/div[2]/div/p[" +
                              Convert.ToString(countParagraph) + "]";
                }
                else
                {
                    break;
                }
            }
        }
        catch (Exception)
        {
            Console.WriteLine(url + Resources.GlobalResources.ResourceManager.GetString("paragraphGetError", CultureInfo.CurrentCulture));
            while (true)
            {
                if (htmlDocument.DocumentNode.SelectSingleNode(paragraphElement) != null)
                {
                    paragraphHtmlNode = htmlDocument.DocumentNode.SelectSingleNode(paragraphElement);
                    break;
                }
                Console.WriteLine(url + Resources.GlobalResources.ResourceManager.GetString("paragraphGetError", CultureInfo.CurrentCulture));
            }
        }

        return text;
    }

    private static string GiveChapterNormalizedText(string text)
    {
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

    private void MergeBookChapterFiles()
    {
        foreach (string chapterLink in _muThDownloaderBook.ChaptersLinks)
        {
            string chapterFilePath = _muThDownloaderBook.Folder + chapterLink.NormalizeUrl() + ".txt";
            string text = File.ReadAllText(chapterFilePath);

            FileMaker.MakeBookFile(text, _muThDownloaderBook.FileName + ".txt");

            if (_muThDownloaderBook.DelFileSelection == "Delete")
            {
                try
                {
                    File.Delete(chapterFilePath);
                }
                catch
                {
                    Console.WriteLine(
                        Resources.GlobalResources.ResourceManager.GetString("chapterFileRemoveFailure",
                            CultureInfo.CurrentCulture) + chapterFilePath);
                }
            }
        }

    }
}