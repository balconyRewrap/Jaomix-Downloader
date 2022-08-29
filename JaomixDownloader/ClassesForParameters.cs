namespace JaomixDownloader
{
    public class TextFile
    {
        protected string _FileName;

        public string FileName
        {
            get => _FileName;
            set => _FileName = value;
        }
    }

    public class MetadataFile : TextFile
    {
        public string BookTitle { get; set; }

        public string AuthorName { get; set; }

        public string Description { get; set; }
    }

    public class BookFile : MetadataFile
    {

        public string BookFileTxt => _FileName + ".txt";

        public string BookFileEpub => _FileName + ".epub";

        public string MetadataFilePath { get; set; }
    }

    public class DownloaderParameters : TextFile
    {
        public string[] ChaptersLinks { get; set; }
    }

    public class SlowDownloaderParameters : DownloaderParameters
    {

    }

    public class MuThDownloaderParameters : DownloaderParameters
    {
        public string Folder { get; set; }
        public string DelFileSelection { get; set; }
    }

    public class MetadataYamlFile : MetadataFile
    {
        public new string FileName = "metadata.yaml";
    }

}
