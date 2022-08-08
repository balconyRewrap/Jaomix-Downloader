namespace Jaomix_Parser;

internal class Choiser
{
    public static void Init(string folder)
    {
        Console.WriteLine("Выберите нужную вам функцию:\n\n ");

        Console.WriteLine("Введите 1, если необходим скачиватель быстрый\n ");

        Console.WriteLine("Введите 2, если необходим парсерс ссылок\n ");

        Console.WriteLine("Введите 3, если необходимо поменять активную папку программы\n ");

        Console.WriteLine("Введите 0, если необходимо выйти\n ");
        string decision = Console.ReadLine();
        switch (decision)
        {
            case "1":
                ChaptDownChoise(folder);
                break;
            case "2":
                LinksDownChoise(folder);
                break;
            case "3":
                ActiveFolderChanger();
                break;
            case "0":
                Environment.Exit(0);
                break;
        }
    }

    private static void ChaptDownChoise(string folder)
    {
        var x = new ChapterDownloader();
        x.Initializer(folder);
    }

    private static void LinksDownChoise(string folder)
    {
        Console.WriteLine("Введите url адрес страницы с книгой");
        LinksDownloader.Downloader(Console.ReadLine(), folder);
    }

    private static void ActiveFolderChanger()
    {
        ConfigGiver.Giver("1");
    }
}