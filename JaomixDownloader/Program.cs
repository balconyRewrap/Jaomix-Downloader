using System.Globalization;

namespace JaomixDownloader;

internal class Program
{
    private static void Main(string[] args)
    {
        var configGiver = new ConfigGiver();
        string language = configGiver.GiveProgramLanguage();
        Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
        string folder = configGiver.GiveActiveFolderPath();
        var chooser = new Chooser(folder);
        chooser.Init();
        Console.WriteLine(@"PROGRAM FINISHED");
        Console.ReadKey();
    }
}