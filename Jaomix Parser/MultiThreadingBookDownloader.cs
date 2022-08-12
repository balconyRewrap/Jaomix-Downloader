using HtmlAgilityPack;

namespace Jaomix_Parser;

public class MultiThreadingBookDownloader
{

    public void MakeBook(string folder, string[] links, string bookFileTxt, string bookFileEpub, string bookName,
        string authorName, int dec, FileMaker fileMaker, string finalFileType, string delFileSelection)
    {
        var chaptersList = new Dictionary<string, string>();
        Parallel.ForEach(links, link =>
        {
            string title = TitleMaker.Maker(link);
            string chapterFile = title.TitleNormalizer() + ".txt";
            chaptersList.Add(link, chapterFile);
            fileMaker.ParrallelMaker(DownloadChapter(link), folder + chapterFile, delFileSelection);
        });
        foreach (string link in links)
        {
            string text = File.ReadAllText(folder + chaptersList[link]);
            fileMaker.Maker(text, bookFileTxt, bookName, authorName, dec);
            dec++;
        }

        Console.WriteLine("Файл книги .txt создан успешно");
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
        string title;
        var web = new HtmlWeb();
        var document = web.Load(url);
        int error = 0;
        // xPath элемента с главой
        string xTitle = "//*[@id=\"dfklje\"]";
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
                Console.WriteLine(error + $" - {url}");
            }
        }

        HtmlNode x;
        // так pandoc (конвертер) распознает название главы
        text += "\n# " + title + "\n \n \n";
        try
        {
            x = document.DocumentNode.SelectSingleNode(element);
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
        }
        catch (Exception)
        {
            Console.WriteLine($"Попытка получить параграф по адресу {url} не получила ничего. Возможно, сайт упал или нет соединения. \nПовторяю попытку. Если данное сообщение будет выводиться слишком долго, закройти программу");
            while (true)
            {
                if (document.DocumentNode.SelectSingleNode(element) != null)
                {
                    x = document.DocumentNode.SelectSingleNode(element);
                    break;
                }
                Console.WriteLine($"Попытка получить параграф по адресу {url} не получила ничего. Возможно, сайт упал или нет соединения. \nПовторяю попытку. Если данное сообщение будет выводиться слишком долго, закройти программу");
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