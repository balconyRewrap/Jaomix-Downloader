namespace JaomixDownloader.ClassesForParameters;

public class BookFile : MetadataFile
{
    public string BookFileTxt => _FileName + ".txt";

    public string BookFileEpub => _FileName + ".epub";

    public string MetadataFilePath { get; set; }
}