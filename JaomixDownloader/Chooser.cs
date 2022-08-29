using System.Text;
using System.Globalization;

namespace JaomixDownloader;

internal class Chooser
{
    private readonly string _folder;
    public Chooser(string folder)
    {
        _folder = folder;
        Init();
    }

    private void Init()
    {
        while (true)
        {

            Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("choicerInitHeader", CultureInfo.CurrentCulture));
            string decision = Console.ReadLine();

            switch (decision)
            {
                case "1":
                    ChooseMultiThreadingBookDownload();
                    break;
                case "2":
                    ChooseLinksParser();
                    break;
                case "3":
                    ChooseSlowBookDownload();
                    break;
                case "4":
                    ChooseTxtToEpubConverter();
                    break;
                case "5":
                    ChooseYamlplusTxtBookMaker();
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

    private void ChooseMultiThreadingBookDownload()
    {
        
        var downloaderParameters = new MuThDownloaderParameters();


        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("linksFileName", CultureInfo.CurrentCulture));
        string linksFile = _folder + CorrectDataGiver.GiveValidTxtFile();

        downloaderParameters.ChaptersLinks = File.ReadAllLines(linksFile, Encoding.UTF8);
        string mainPageUrl = downloaderParameters.ChaptersLinks[0].GiveMainUrlFromDerivative();

        var book = new BookFile();
        var bookMetadata = new JaomixMetadata();

        var metadataYaml = new MetadataYamlFile
        {
            BookTitle = bookMetadata.GiveBookName(mainPageUrl),
            AuthorName = bookMetadata.GiveAuthorName(mainPageUrl),
            Description = bookMetadata.GiveBookDescription(mainPageUrl),
        }
        ;
        FileMaker.MetaDataYamlFileMaker(_folder, metadataYaml);
        book.MetadataFilePath = _folder + metadataYaml.FileName;


        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookFileNameNoExtensions", CultureInfo.CurrentCulture));
        downloaderParameters.FileName = book.FileName = _folder + Console.ReadLine();

        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("delTempFiles", CultureInfo.CurrentCulture));

        downloaderParameters.DelFileSelection = (Console.ReadLine() != "1") ? "Delete" : "NotDelete";

        downloaderParameters.Folder = _folder;

        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("finalFileExtension", CultureInfo.CurrentCulture));
        string finalFileType = (Console.ReadLine() != "1") ? "epub" : "txt";

        var bookDownloader = new MultiThreadingBookDownloader(downloaderParameters);
        bookDownloader.MakeBook();

        if (finalFileType == "epub")
        {
            var txtToEpubConverter = new TxtToEpubConverter();
            txtToEpubConverter.MakeBook(book);
        }

    }

    private void ChooseLinksParser()
    {

        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookUrl", CultureInfo.CurrentCulture));
        string bookUrl = CorrectDataGiver.GiveValidJaomixLink();
        LinksDownloader.ParseLinksToFile(bookUrl, _folder);
    }

    private void ChooseSlowBookDownload()
    {

        var downloaderParameters = new SlowDownloaderParameters();


        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("linksFileName", CultureInfo.CurrentCulture));
        string linksFile = _folder + CorrectDataGiver.GiveValidTxtFile();
        downloaderParameters.ChaptersLinks = File.ReadAllLines(linksFile, Encoding.UTF8);
        string mainPageUrl = downloaderParameters.ChaptersLinks[0].GiveMainUrlFromDerivative();

        var book = new BookFile();
        var bookMetadata = new JaomixMetadata();
        var metadataYaml = new MetadataYamlFile
        {
            BookTitle = bookMetadata.GiveBookName(mainPageUrl),
            AuthorName = bookMetadata.GiveAuthorName(mainPageUrl),
            Description = bookMetadata.GiveBookDescription(mainPageUrl)
        };

        FileMaker.MetaDataYamlFileMaker(_folder, metadataYaml);

        book.MetadataFilePath = _folder + metadataYaml.FileName;
        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookFileNameNoExtensions", CultureInfo.CurrentCulture));
        downloaderParameters.FileName = book.FileName = _folder + Console.ReadLine();


        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("finalFileExtension", CultureInfo.CurrentCulture));
        string finalFileType = (Console.ReadLine() != "1") ? "epub" : "txt";

        var bookDownloader = new SlowBookDownloader(downloaderParameters);
        bookDownloader.MakeBook();

        if (finalFileType == "epub")
        {
            var txtToEpubConverter = new TxtToEpubConverter();
            txtToEpubConverter.MakeBook(book);
        }
    }

    private void ChooseTxtToEpubConverter()
    {
        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookFileNameTXT", CultureInfo.CurrentCulture));
        string bookFileTxt = _folder + Console.ReadLine();

        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookFileNameEPUB", CultureInfo.CurrentCulture));
        string bookFileEpub = _folder + Console.ReadLine();

        var txtToEpubConverter = new TxtToEpubConverter();
        txtToEpubConverter.Convert(bookFileTxt, bookFileEpub);
    }

    private void ChooseYamlplusTxtBookMaker()
    {
        var book = new BookFile();

        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("bookFileNameNoExtensions", CultureInfo.CurrentCulture));
        book.FileName = _folder + Console.ReadLine();
        book.MetadataFilePath = $"{_folder}metadata.yaml";

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