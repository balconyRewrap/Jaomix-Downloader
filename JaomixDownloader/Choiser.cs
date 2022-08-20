using System.Text;
using System.Globalization;
using System.Resources;

namespace JaomixDownloader;

internal class Choiser
{
    public static void Init(string folder)
    {
        while (true)
        {

            Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("choicerInitHeader", CultureInfo.CurrentCulture));
            string decision = Console.ReadLine();

            switch (decision)
            {
                case "1":
                    ChooseMultiThreadingBookDownload(folder);
                    break;
                case "2":
                    ChooseLinksParser(folder);
                    break;
                case "3":
                    ChooseSlowBookDownload(folder);
                    break;
                case "4":
                    ChooseTxtToEpubConverter(folder);
                    break;
                case "5":
                    ChooseActiveFolderChange(folder);
                    break;
                case "6":
                    ChooseLanguageChange(folder);
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private static void ChooseMultiThreadingBookDownload(string folder)
    {
        var bookDownloader = new MultiThreadingBookDownloader();
        var downloaderParamsSaver = new MtDownloaderParamsSaver();


        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("linksFileName", CultureInfo.CurrentCulture));
        string linksFile = folder + Console.ReadLine();
        downloaderParamsSaver.ChaptersLinks = File.ReadAllLines(linksFile, Encoding.UTF8);
        string mainPageUrl = downloaderParamsSaver.ChaptersLinks[0].GiveMainUrlFromDerivative();

        var book = new Book();
        var bookMetadata = new JaomixMetadata();

        book.Title = bookMetadata.GiveBookName(mainPageUrl);
        book.AuthorName = bookMetadata.GiveAuthorName(mainPageUrl);
        book.Description = bookMetadata.GiveBookDescription(mainPageUrl);

        FileMaker.MetaDataYamlFileMaker(folder, book.Title, book.AuthorName, book.Description);
        book.MetadataFilePath = folder + "metadata.yaml";


        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookFileNameNoExtensions", CultureInfo.CurrentCulture));
        downloaderParamsSaver.textFileName = book.textFileName = folder + Console.ReadLine();

        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("delTempFiles", CultureInfo.CurrentCulture));
        downloaderParamsSaver.DelFileSelection = Console.ReadLine();

        downloaderParamsSaver.Folder = folder;

        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("finalFileExtension", CultureInfo.CurrentCulture));
        string finalFileType = Console.ReadLine();


        bookDownloader.MakeBook(downloaderParamsSaver);

        if (finalFileType != "1") TxtToEpubConverter.MakeBook(book);

    }

    private static void ChooseSlowBookDownload(string folder)
    {
        var bookDownloader = new SlowBookDownloader();
        var downloaderParamsSaver = new SlowDownloaderParamsSaver();


        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("linksFileName", CultureInfo.CurrentCulture));
        string linksFile = folder + Console.ReadLine();
        downloaderParamsSaver.ChaptersLinks = File.ReadAllLines(linksFile, Encoding.UTF8);
        string mainPageUrl = downloaderParamsSaver.ChaptersLinks[0].GiveMainUrlFromDerivative();

        var book = new Book();
        var bookMetadata = new JaomixMetadata();

        book.Title = bookMetadata.GiveBookName(mainPageUrl);
        book.AuthorName=bookMetadata.GiveAuthorName(mainPageUrl);
        book.Description=bookMetadata.GiveBookDescription(mainPageUrl);

        FileMaker.MetaDataYamlFileMaker(folder,book.Title,book.AuthorName,book.Description);
        book.MetadataFilePath = folder + "metadata.yaml";


        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookFileNameNoExtensions", CultureInfo.CurrentCulture));
        downloaderParamsSaver.textFileName = book.textFileName = folder + Console.ReadLine();


        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("finalFileExtension", CultureInfo.CurrentCulture));
        string finalFileType = Console.ReadLine();


        bookDownloader.MakeBook(downloaderParamsSaver);

        if (finalFileType != "1") TxtToEpubConverter.MakeBook(book);
    }

    private static void ChooseLinksParser(string folder)
    {
        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookUrl", CultureInfo.CurrentCulture));
        LinksDownloader.ParseLinksToFile(Console.ReadLine(), folder);
    }

    private static void ChooseActiveFolderChange(string folder)
    {
        ConfigGiver configGiver = new ConfigGiver();

        configGiver.ChangeActiveFolderPath();
    }

    private static void ChooseLanguageChange(string folder)
    {
        ConfigGiver configGiver = new ConfigGiver();

        configGiver.ChangeProgramLanguage();
        string language = configGiver.GiveProgramLanguage();
        Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
    }

    private static void ChooseTxtToEpubConverter(string folder)
    {
        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookFileNameTXT", CultureInfo.CurrentCulture));
        string bookFileTxt = folder + Console.ReadLine();

        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookFileNameEPUB", CultureInfo.CurrentCulture));
        string bookFileEpub = folder + Console.ReadLine();

        TxtToEpubConverter.Convert(bookFileTxt, bookFileEpub);
    }
}