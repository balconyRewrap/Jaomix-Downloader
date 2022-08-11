using System.Text;

namespace Jaomix_Parser;

internal class FileMaker
{
    public void Maker(List<string> urls, string path)
    {
        Console.WriteLine("LINKS LIST MAKER STARTED");

        urls.Reverse();
        var linksList = new List<string>();
        foreach (string element in urls)
            if (!linksList.Contains(element))
                linksList.Add(element);
        var linksListFile = new StreamWriter(path, true, Encoding.UTF8);
        foreach (string element in linksList)
        {
            try
            {
                linksListFile.WriteLine(element);
                Console.WriteLine(element);
            }
            catch
            {
                Console.WriteLine("Не был записан в файл " + element);
            }
        }

        linksListFile.Close();
        Console.WriteLine("LINKS LIST MAKER FINISHED");
    }

    public void Maker(string text, string path, string bookName, string authorName, int dec)
    {
        Console.WriteLine("BOOK MAKER STARTED");
        var textFile = new StreamWriter(path, true, Encoding.UTF8);

        try
        {
            if (dec == 1)
            {
                // так pandoc понимает, что это имя автора и название книги
                textFile.WriteLine($"% {bookName} \n");
                textFile.WriteLine($"% {authorName} \n");
            }

            textFile.WriteLine(text);
        }
        catch
        {
            Console.WriteLine("Не был записан в файл ");
        }

        textFile.Close();
        Console.WriteLine("BOOK MAKER FINISHED");
    }

    public void ParrallelMaker(string text, string path)
    {
        var textFile = new StreamWriter(path, true, Encoding.UTF8);

        try
        {
            textFile.WriteLine(text);
        }
        catch
        {
            Console.WriteLine("Не был записан в файл ");
        }

        textFile.Close();
    }
}