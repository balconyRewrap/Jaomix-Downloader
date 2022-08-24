using HtmlAgilityPack;

namespace JaomixDownloader;
public class JaomixMetadata
{ 
    HtmlWeb _web = new HtmlWeb();
    private const string BookNameXPath = @"/html/body/div/div[3]/div/div/div/div/div[1]/div[2]/h1";

    private const string AuthorNameXPath = @"/html/body/div/div[3]/div/div/div/div/div[1]/div[2]/div[5]/div/div[1]/p[1]";
    
    private const string BookDescriptionXPath = @"//*[@id=""desc-tab""]";
    
    private const string ChapterTitleXPath = @"//*[@id=""dfklje""]";

    public string GiveBookName(string url)
    {
        var document = _web.Load(url);
        string bookName;
        try
        {
            bookName = document.DocumentNode.SelectSingleNode(BookNameXPath).InnerText;
        }
        catch (Exception)

        {

            while (true)
            {
                document = _web.Load(url);
                Thread.Sleep(4000);
                if (document.DocumentNode.SelectSingleNode(BookNameXPath) != null)
                {
                    bookName = document.DocumentNode.SelectSingleNode(BookNameXPath).InnerText;
                    break;
                }

            }
        }
        return bookName;
    }
    
    public string GiveAuthorName(string url)
    {
        var document = _web.Load(url);
        string authorName ="";
        try
        {
            authorName = document.DocumentNode.SelectSingleNode(AuthorNameXPath).InnerText;
        }
        catch (Exception)

        {

            while (true)
            {
                document = _web.Load(url);
                Thread.Sleep(4000);
                if (document.DocumentNode.SelectSingleNode(AuthorNameXPath) != null)
                {
                    authorName = document.DocumentNode.SelectSingleNode(AuthorNameXPath).InnerText;
                    break;
                }

            }
        }

        authorName = authorName[7..];
        return authorName;
    }
    
    public string GiveBookDescription(string url)
    {
        var document = _web.Load(url);
        string bookDescription = "";
        try
        {
            bookDescription = document.DocumentNode.SelectSingleNode(BookDescriptionXPath).InnerText;
        }
        catch (Exception)

        {

            while (true)
            {
                document = _web.Load(url);
                Thread.Sleep(4000);
                if (document.DocumentNode.SelectSingleNode(BookDescriptionXPath) != null)
                {
                    bookDescription = document.DocumentNode.SelectSingleNode(BookDescriptionXPath).InnerText;
                    break;
                }

            }
        }

        return bookDescription;
    }
    
    public string GiveChapterTitle(string url)
    {
        var htmlDocument = _web.Load(url);
        string chapterTitle;
        try
        {
            chapterTitle = htmlDocument.DocumentNode.SelectSingleNode(ChapterTitleXPath).InnerText;
        }
        catch (Exception)

        {

            while (true)
            {
                htmlDocument = _web.Load(url);
                Thread.Sleep(4000);
                if (htmlDocument.DocumentNode.SelectSingleNode(ChapterTitleXPath) != null)
                {
                    chapterTitle = htmlDocument.DocumentNode.SelectSingleNode(ChapterTitleXPath).InnerText;
                    break;
                }

            }
        }
        return chapterTitle;
    }
    
    public static string GiveChapterTitle(HtmlWeb web, string url)
    {
        var htmlDocument = web.Load(url);
        string chapterTitle;
        try
        {
            chapterTitle = htmlDocument.DocumentNode.SelectSingleNode(ChapterTitleXPath).InnerText;
        }
        catch (Exception)

        {

            while (true)
            {
                htmlDocument = web.Load(url);
                Thread.Sleep(4000);
                if (htmlDocument.DocumentNode.SelectSingleNode(ChapterTitleXPath) != null)
                {
                    chapterTitle = htmlDocument.DocumentNode.SelectSingleNode(ChapterTitleXPath).InnerText;
                    break;
                }

            }
        }
        return chapterTitle;
    }
}