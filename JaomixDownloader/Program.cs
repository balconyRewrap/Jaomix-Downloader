using System.Globalization;


namespace JaomixDownloader;

internal class Program
{

    private static void Main(string[] args)
    {

        ConfigGiver configGiver = new ConfigGiver();
        string language = configGiver.GiveProgramLanguage();
        Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
        string folder = configGiver.GiveActiveFolderPath();
        ConfigGiver.GiveOS();
        Choiser.Init(folder);
        Console.WriteLine(@"PROGRAM FINISHED");
        Console.ReadKey();
    }

}