using HtmlAgilityPack;
namespace Jaomix_Parser;

internal class Program
{
    private static void Main(string[] args)
    {
        IConfigGiver configGiver = new ConfigGiver();
        string folder = configGiver.Giver();
        Console.WriteLine("Введите название изначального файла .txt, например text.txt");
        string bookFileTxt = folder + Console.ReadLine();
        Console.WriteLine("Введите название конечного файла .epub, например text.epub");
        string bookFileEpub = folder + Console.ReadLine();
        TxtToEpubConverter.Converter(bookFileTxt, bookFileEpub);
        /*        Choiser.Init(folder);*/

        Console.WriteLine("PROGRAM FINISHED");

        Console.ReadKey();

}
}