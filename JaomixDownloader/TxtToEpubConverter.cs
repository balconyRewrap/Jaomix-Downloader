namespace JaomixDownloader
{
    public class TxtToEpubConverter
    {
        public static void Convert(string txtFilePath, string epubFilePath)
        {
            string strCmdText = $"/C pandoc {txtFilePath} -o {epubFilePath}";
            System.Diagnostics.Process.Start("powershell.exe", strCmdText);
        }

        public static void MakeBook(Book book)
        {
            string strCmdText = $"/C pandoc {book.BookFileTxt} {book.MetadataFilePath} -o {book.BookFileEpub}";
            System.Diagnostics.Process.Start("powershell.exe", strCmdText);
        }
    }
}
