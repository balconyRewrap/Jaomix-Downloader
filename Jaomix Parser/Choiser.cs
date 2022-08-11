using System.Text;

namespace Jaomix_Parser;

internal class Choiser
{
    public static void Init(string folder)
    {
        Console.WriteLine("Выберите нужную вам функцию:\n\n ");

        Console.WriteLine("Введите 1, если необходим скачиватель глав быстрый\n ");

        Console.WriteLine("Введите 2, если необходим парсерс ссылок\n ");

        Console.WriteLine("Введите 3, если необходимо поменять активную папку программы\n ");

        Console.WriteLine("Введите 4, если необходим скачиватель глав медленный\n ");

        Console.WriteLine("Введите 5, если необходим конвертер из txt в epub\n ");

        Console.WriteLine("Введите 0, если необходимо выйти\n ");
        string decision = Console.ReadLine();
        switch (decision)
        {
            case "1":
                MultiThreadingChaptDownChoise(folder);
                break;
            case "2":
                LinksDownChoise(folder);
                break;
            case "3":
                ActiveFolderChangerChoise(folder);
                break;
            case "4":
                ChaptDownChoise(folder);
                break;
            case "5":
                ChaptDownChoise(folder);
                break;
            case "0":
                Environment.Exit(0);
                break;
        }
    }

    private static void MultiThreadingChaptDownChoise(string folder)
    {
        Console.WriteLine("Введите название файла, куда запишется книга (без .txt)");
        string bookFile = Console.ReadLine();
        string bookFileTxt = folder + bookFile + ".txt";
        string bookFileEpub = folder + bookFile + ".epub";

        Console.WriteLine(
            "Введите название файла, где находятся ссылки (с .txt) \n Он должен находиться в папке, которую вы задали в начале");
        string linksFile = folder + Console.ReadLine();

        Console.WriteLine("Введите название книги");
        string bookName = Console.ReadLine();

        Console.WriteLine("Введите имя автора");
        string authorName = Console.ReadLine();

        Console.WriteLine("Какой итоговый файл необходим?");
        Console.WriteLine("Введите 1, если необходим .epub\n ");
        Console.WriteLine("Введите 2, если необходим .txt\n ");
        string finalFileType = Console.ReadLine();

        string[] links = File.ReadAllLines(linksFile, Encoding.UTF8);
        int dec = 1;
        FileMaker fileMaker = new FileMaker();
        var x = new MultiThreadingBookDownloader();
        x.MakeBook(folder, links, bookFileTxt, bookFileEpub, bookName, authorName, dec, fileMaker, finalFileType);
        Init(folder);
    }

    private static void ChaptDownChoise(string folder)
    {
        var x = new SlowBookDownloader();
        Console.WriteLine("Введите название файла, куда запишется книга (без .txt)");
        string bookFile = Console.ReadLine();
        string bookFileTxt = folder + bookFile + ".txt";
        string bookFileEpub = folder + bookFile + ".epub";
        Console.WriteLine(
            "Введите название файла, где находятся ссылки (с .txt) \n Он должен находиться в папке, которую вы задали в начале");
        string linksFile = folder + Console.ReadLine();
        Console.WriteLine("Введите название книги");
        string bookName = Console.ReadLine();
        Console.WriteLine("Введите имя автора");
        string authorName = Console.ReadLine();

        Console.WriteLine("Какой итоговый файл необходим?");
        Console.WriteLine("Введите 1, если необходим .epub\n ");
        Console.WriteLine("Введите 2, если необходим .txt\n ");
        string finalFileType = Console.ReadLine();

        string[] links = File.ReadAllLines(linksFile, Encoding.UTF8);
        int dec = 1;
        FileMaker fileMaker = new FileMaker();
        x.MakeBook(folder, links, bookFileTxt, bookFileEpub, bookName, authorName, dec, fileMaker, finalFileType);
        Init(folder);
    }

    private static void LinksDownChoise(string folder)
    {
        Console.WriteLine("Введите url адрес страницы с книгой");
        LinksDownloader.Downloader(Console.ReadLine(), folder);
        Init(folder);
    }

    private static void ActiveFolderChangerChoise(string folder)
    {
        ConfigGiver configGiver = new ConfigGiver();

        configGiver.Changer();
        Init(folder);

    }
    private static void TxtToEpubConverterChoise(string folder)
    {
        Console.WriteLine("Введите название изначального файла .txt, например text.txt");
        string bookFileTxt = folder + Console.ReadLine() + ".txt";
        Console.WriteLine("Введите название конечного файла .epub, например text.epub");
        string bookFileEpub = folder + Console.ReadLine() + ".epub";
        TxtToEpubConverter.Converter(bookFileTxt,bookFileEpub);
        Init(folder);

    }
}