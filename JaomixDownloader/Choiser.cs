using System.Text;
using System.Globalization;

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
                    ChooseYamlnTxtBookMaker(folder);
                    break;
                case "6":
                    ChooseActiveFolderChange();
                    break;
                case "7":
                    ChooseLanguageChange();
                    break;
                case "8":
                    ChooseOSChange();
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

        var book = new BookFile();
        var metadataYaml = new MetadataFileYamlFileParamsSaver();
        var bookMetadata = new JaomixMetadata();

        metadataYaml.BookTitle = bookMetadata.GiveBookName(mainPageUrl);
        metadataYaml.AuthorName = bookMetadata.GiveAuthorName(mainPageUrl);
        metadataYaml.Description = bookMetadata.GiveBookDescription(mainPageUrl);

        FileMaker.MetaDataYamlFileMaker(folder, metadataYaml);
        book.MetadataFilePath = folder + metadataYaml.FileName;


        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookFileNameNoExtensions", CultureInfo.CurrentCulture));
        downloaderParamsSaver.FileName = book.FileName = folder + Console.ReadLine();

        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("delTempFiles", CultureInfo.CurrentCulture));
        downloaderParamsSaver.DelFileSelection = Console.ReadLine();

        downloaderParamsSaver.Folder = folder;

        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("finalFileExtension", CultureInfo.CurrentCulture));
        string finalFileType = Console.ReadLine();


        bookDownloader.MakeBook(downloaderParamsSaver);

        if (finalFileType != "1")
        {
            var txtToEpubConverter = new TxtToEpubConverter();
            txtToEpubConverter.MakeBook(book);
        }

    }

    private static void ChooseLinksParser(string folder)
    {
        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookUrl", CultureInfo.CurrentCulture));
        LinksDownloader.ParseLinksToFile(Console.ReadLine(), folder);
    }

    private static void ChooseSlowBookDownload(string folder)
    {
        var bookDownloader = new SlowBookDownloader();
        var downloaderParamsSaver = new SlowDownloaderParamsSaver();


        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("linksFileName", CultureInfo.CurrentCulture));
        string linksFile = folder + Console.ReadLine();
        downloaderParamsSaver.ChaptersLinks = File.ReadAllLines(linksFile, Encoding.UTF8);
        string mainPageUrl = downloaderParamsSaver.ChaptersLinks[0].GiveMainUrlFromDerivative();

        var book = new BookFile();
        var bookMetadata = new JaomixMetadata();
        var metadataYaml = new MetadataFileYamlFileParamsSaver
        {
            BookTitle = bookMetadata.GiveBookName(mainPageUrl),
            AuthorName = bookMetadata.GiveAuthorName(mainPageUrl),
            Description = bookMetadata.GiveBookDescription(mainPageUrl)
        };

        FileMaker.MetaDataYamlFileMaker(folder, metadataYaml);

        book.MetadataFilePath = folder + metadataYaml.FileName;
        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookFileNameNoExtensions", CultureInfo.CurrentCulture));
        downloaderParamsSaver.FileName = book.FileName = folder + Console.ReadLine();


        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("finalFileExtension", CultureInfo.CurrentCulture));
        string finalFileType = Console.ReadLine();


        bookDownloader.MakeBook(downloaderParamsSaver);

        if (finalFileType != "1")
        {
            var txtToEpubConverter = new TxtToEpubConverter();
            txtToEpubConverter.MakeBook(book);
        }
    }

    private static void ChooseTxtToEpubConverter(string folder)
    {
        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookFileNameTXT", CultureInfo.CurrentCulture));
        string bookFileTxt = folder + Console.ReadLine();

        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookFileNameEPUB", CultureInfo.CurrentCulture));
        string bookFileEpub = folder + Console.ReadLine();

        var txtToEpubConverter = new TxtToEpubConverter();
        txtToEpubConverter.Convert(bookFileTxt, bookFileEpub);
    }

    private static void ChooseYamlnTxtBookMaker(string folder)
    {
        var book = new BookFile();

        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookFileNameNoExtensions", CultureInfo.CurrentCulture));
        book.FileName = folder + Console.ReadLine();
        book.MetadataFilePath = $"{folder}metadata.yaml";

        var txtToEpubConverter = new TxtToEpubConverter();
        txtToEpubConverter.MakeBook(book);
    }

    private static void ChooseActiveFolderChange()
    {
        var configGiver = new ConfigGiver();
        configGiver.ChangeActiveFolderPath();
    }

    private static void ChooseLanguageChange()
    {
        var configGiver = new ConfigGiver();
        configGiver.ChangeProgramLanguage();

        string language = configGiver.GiveProgramLanguage();
        Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
    }

    private static void ChooseOSChange()
    {
        var configGiver = new ConfigGiver();
        configGiver.ChangeOS();
    }

}