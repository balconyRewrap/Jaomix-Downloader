namespace Jaomix_Parser;

internal class Program
{
    /*def windows_string_replace(str):
str = str.replace('\\', '')
str = str.replace('|', '')
str = str.replace('/', '')
str = str.replace(':', '')
str = str.replace('*', '')
str = str.replace('?', '')
str = str.replace('"', '')
str = str.replace('<', '')
str = str.replace('>', '')
return str*/
    private static void Main(string[] args)
    {
        var folder = ConfigGiver.Giver();
/*        MultiThreadingChapterDownloader  parDownloader = new MultiThreadingChapterDownloader();
            parDownloader.Initializer(folder);*/
        TxtToEpubConverter.Converter(folder+"testName.txt",folder+"testNane.epub");
        Console.WriteLine("PROGRAM FINISHED");
        Console.ReadKey();
    }
}