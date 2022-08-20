using System.Text;
using YamlDotNet.RepresentationModel;


namespace JaomixDownloader;
public class FileMaker
{
    public static void MakeLinksFile(List<string> urls, string path)
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

    public static void MakeBookFile(string text, string path)
    {
        Console.WriteLine("BOOK MAKER STARTED");
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
        Console.WriteLine("BOOK MAKER FINISHED");
    }

    public static void MakeChapterFile(string text, string path)
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

    public static void MetaDataYamlFileMaker(string folder, string bookName, string authorName, string description)
    {
        const string initialContent = "---\nversion: 1\n...";

        var stringReader = new StringReader(initialContent);
        var stream = new YamlStream();
        stream.Load(stringReader);
        var rootMappingNode = (YamlMappingNode)stream.Documents[0].RootNode;

        var titleProps = new YamlMappingNode();
        titleProps.Add("type", "main");
        titleProps.Add("text", bookName);
        rootMappingNode.Add("title", titleProps);

        var authorProps = new YamlMappingNode();
        authorProps.Add("role", "author");
        authorProps.Add("text", authorName);
        rootMappingNode.Add("creator", authorProps);

        rootMappingNode.Add("description",description);

        using (TextWriter writer = File.CreateText(folder + "metadata.yaml"))
        {
            writer.WriteLine("---");
            stream.Save(writer, false);
        }
    }
}