using HtmlAgilityPack;
using System.Globalization;

namespace JaomixDownloader;

public class MultiThreadingBookDownloader
{
    public void MakeBook(MtDownloaderParamsSaver mtDownloaderParamsSaver)
    {


        Parallel.ForEach(mtDownloaderParamsSaver.ChaptersLinks, chapterLink =>
        {
            Console.WriteLine(chapterLink);
            string chapterFile = chapterLink.NormalizeUrl() + ".txt";
            string chapterRawText = "";
            string chapterText = "";

            
            // Регулярно вылетает ошибка, связанная с неправильной загрузкой страницы.
            // Решается простым перезапуском метода
            while(chapterRawText=="")
            {
                try
                {
                    chapterRawText = GiveChapterRawText(chapterLink);
                }
                catch (Exception e)
                {
                    Console.WriteLine("2");
                    Console.WriteLine(e);
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
                FileMaker.MakeChapterFile(chapterText, mtDownloaderParamsSaver.Folder + chapterFile);
            }
            catch (Exception e)
            {
                Console.WriteLine("4");
                Console.WriteLine(e);
            }


        });

        foreach (string chapterLink in mtDownloaderParamsSaver.ChaptersLinks)
        {
            string chapterFilePath = mtDownloaderParamsSaver.Folder + chapterLink.NormalizeUrl() + ".txt";
            string text = File.ReadAllText(chapterFilePath);
        
            FileMaker.MakeBookFile(text, mtDownloaderParamsSaver.textFileName + ".txt");
            if (mtDownloaderParamsSaver.DelFileSelection != "1")
            {
                try
                {
                    File.Delete(chapterFilePath);
                }
                catch
                {
                    Console.WriteLine(GlobalStrings.ResourceManager.GetString("chapterFileRemoveFailure", CultureInfo.CurrentCulture) + chapterFilePath);
                }
        
            }
        }

        Console.WriteLine(GlobalStrings.ResourceManager.GetString("bookTxtMakeSuccess", CultureInfo.CurrentCulture));

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

        int error = 0;

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
            Console.WriteLine(url+GlobalStrings.ResourceManager.GetString("paragraphGetError", CultureInfo.CurrentCulture));
            while (true)
            {
                if (htmlDocument.DocumentNode.SelectSingleNode(paragraphElement) != null)
                {
                    paragraphHtmlNode = htmlDocument.DocumentNode.SelectSingleNode(paragraphElement);
                    break;
                }
                Console.WriteLine(url + GlobalStrings.ResourceManager.GetString("paragraphGetError", CultureInfo.CurrentCulture));
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
}