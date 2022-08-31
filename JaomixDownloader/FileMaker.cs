using System.Text;
using JaomixDownloader.ClassesForParameters;
using JaomixDownloader.Resources;
using YamlDotNet.RepresentationModel;

namespace JaomixDownloader;

public class FileMaker
{
    public static void MakeLinksFile(List<string> urls, string path)
    {
        Console.WriteLine(GlobalResources.linksListMakerStart);

        urls.Reverse();
        var linksList = new List<string>();
        foreach (string element in urls.Where(element => !linksList.Contains(element))) linksList.Add(element);
        var linksListFile = new StreamWriter(path, true, Encoding.UTF8);
        foreach (string element in linksList)
            try
            {
                linksListFile.WriteLine(element);
                Console.WriteLine(element);
            }
            catch
            {
                Console.WriteLine(element + GlobalResources.linkWriteInFileError);
            }

        linksListFile.Close();
        Console.WriteLine(GlobalResources.linksListMakerFinish);
    }

    public static void MakeBookFile(string text, string path)
    {
        Console.WriteLine(GlobalResources.bookMakerStart);
        var textFile = new StreamWriter(path, true, Encoding.UTF8);

        try
        {
            textFile.WriteLine(text);
        }
        catch
        {
            Console.WriteLine(GlobalResources.linkWriteInFileError);
        }

        textFile.Close();
        Console.WriteLine(GlobalResources.bookMakerFinish);
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
            Console.WriteLine(GlobalResources.linkWriteInFileError);
        }

        textFile.Close();
    }

    public static void MetaDataYamlFileMaker(string folder, MetadataYamlFile metadataYamlFileYamlFile)
    {
        const string initialContent = "---\nversion: 1\n...";

        var stringReader = new StringReader(initialContent);
        var stream = new YamlStream();
        stream.Load(stringReader);
        var rootMappingNode = (YamlMappingNode)stream.Documents[0].RootNode;

        var titleProps = new YamlMappingNode();
        titleProps.Add("type", "main");
        titleProps.Add("text", metadataYamlFileYamlFile.BookTitle);
        rootMappingNode.Add("title", titleProps);

        var authorProps = new YamlMappingNode();
        authorProps.Add("role", "author");
        authorProps.Add("text", metadataYamlFileYamlFile.AuthorName);
        rootMappingNode.Add("creator", authorProps);

        rootMappingNode.Add("description", metadataYamlFileYamlFile.Description);

        using (TextWriter writer = File.CreateText(folder + metadataYamlFileYamlFile.FileName))
        {
            writer.WriteLine("---");
            stream.Save(writer, false);
        }
    }
}