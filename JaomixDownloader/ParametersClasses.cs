namespace JaomixDownloader
{
    public class TextFile
    {
        protected string _textFileName;
        public string textFileName { 
            get => _textFileName;
            set => _textFileName = value;
        }
    }

    public class Book:TextFile
    {

        public string BookFileTxt => _textFileName +".txt";

        public string BookFileEpub => _textFileName + ".epub";

        public string MetadataFilePath { get; set; }

        public string Title { get; set; }

        public string AuthorName { get; set; }

        public string Description { get; set; }
    }

    public class DownloaderParamsSaver : TextFile
    {
        public string[] ChaptersLinks { get; set; }
    }

    public class SlowDownloaderParamsSaver : DownloaderParamsSaver
    {

    }

    public class MtDownloaderParamsSaver : DownloaderParamsSaver
    {
        public string Folder { get; set; }
        public string DelFileSelection { get; set; }
    }
}
