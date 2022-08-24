using HtmlAgilityPack;

namespace JaomixDownloader;

public class SlowBookDownloader
{
    public void MakeBook(SlowDownloaderParamsSaver slowDownloaderParamsSaver)
    {
        int chapterCount = 1;
        var titleGiver = new JaomixMetadata();
        foreach (string chapterLink in slowDownloaderParamsSaver.ChaptersLinks)
        {
            string rawText = GiveChapterRawText(chapterLink, titleGiver);
            string text = GiveChapterNormalizedText(rawText);
            FileMaker.MakeBookFile(text, slowDownloaderParamsSaver.FileName + ".txt");
            Console.WriteLine(chapterCount);
            chapterCount++;
        }

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

    private static string GiveChapterRawText(string url, JaomixMetadata titleGiver)
    { 
        int paragraphCount = 1;
        string title = titleGiver.GiveChapterTitle(url);

        // так pandoc (конвертер) распознает название главы
        string text = "\n# " + title + "\n \n \n";

        string paragraphElement = "/html/body/div[1]/div[3]/div/div/div/div/article/div/div[2]/div/p[" +
                          Convert.ToString(paragraphCount) + "]";
        var web = new HtmlWeb();
        var htmlDocument = web.Load(url);

        // Цикл прерывается, как только заканчиваются параграфы текста
        while (true)
        {
            var paragraphHtmlNode = htmlDocument.DocumentNode.SelectSingleNode(paragraphElement);
            if (paragraphHtmlNode != null)
            {
                string paragraph = paragraphHtmlNode.InnerText;

                // epub необходимо два символа перехода для нового параграфа
                if (!paragraph.IsUselessParagraph()) text += paragraph + "\n \n";

                paragraphCount++;
                paragraphElement = "/html/body/div[1]/div[3]/div/div/div/div/article/div/div[2]/div/p[" +
                          Convert.ToString(paragraphCount) + "]";
            }
            else
            {
                break;
            }
        }

        return text;
    }
}