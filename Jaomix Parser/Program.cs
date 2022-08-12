using HtmlAgilityPack;
using System.Text;
namespace Jaomix_Parser;

internal class Program
{
    private static void Main(string[] args)
    {
        IConfigGiver configGiver = new ConfigGiver();
        string folder = configGiver.Giver();
        Choiser.Init(folder);


        Console.WriteLine("PROGRAM FINISHED");

        Console.ReadKey();

}
}