namespace Jaomix_Parser;

internal class Program
{
    private static void Main(string[] args)
    {
        var folder = ConfigGiver.Giver();
        Choiser.Init(folder);
        Console.WriteLine("PROGRAM FINISHED");
        Console.ReadKey();
    }
}