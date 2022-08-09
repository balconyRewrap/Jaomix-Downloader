using HtmlAgilityPack;

namespace Jaomix_Parser;

internal class MultiThreadingChapterDownloader : ChapterDownloader
{
    private void Starter(string folder, string[] links, string bookFileTxt, string bookFileEpub, string bookName,
        string authorName, int dec, FileMaker fileMaker)
    {
        var chaptersList = new Dictionary<string, string>();
        Parallel.ForEach(links, link =>
        {
            string title = Title.TitleMaker(link);
            string chapterFile = Title.TitleNormalizer(title) + ".txt";
            chaptersList.Add(link, chapterFile);
            fileMaker.ParrallelMaker(Downloader(link), folder + chapterFile);
        });
        foreach (string link in links)
        {
            string text = File.ReadAllText(folder + chaptersList[link]);
            fileMaker.Maker(text, bookFileTxt, bookName, authorName, dec);
            dec++;
        }

        TxtToEpubConverter.Converter(folder + bookFileTxt, bookFileEpub);
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
        // Мог бы использовать title из Initialization, но он иногда каким-то образом их другого потока получает title и получается мешанина в книге
        string title = document.DocumentNode.SelectSingleNode(xTitle).InnerText;
        // так pandoc (конвертер) распознает название главы
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
                // epub необходимо два символа перехода для нового параграфа
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