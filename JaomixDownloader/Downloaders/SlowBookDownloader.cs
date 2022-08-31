using HtmlAgilityPack;
using JaomixDownloader.ClassesForParameters;

namespace JaomixDownloader.Downloaders;

public class SlowBookDownloader
{
    private readonly BookDownloaderParameters _book;

    public SlowBookDownloader(BookDownloaderParameters bookData)
    {
        _book = bookData;
    }

    public void MakeBook()
    {
        int chapterCount = 1;
        var titleGiver = new JaomixMetadataGiver();
        foreach (string chapterLink in _book.ChaptersLinks)
        {
            string rawText = GiveChapterRawText(chapterLink, titleGiver);
            string text = GiveChapterNormalizedText(rawText);
            FileMaker.MakeBookFile(text, _book.FileName + ".txt");
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

    private static string GiveChapterRawText(string url, JaomixMetadataGiver titleGiver)
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