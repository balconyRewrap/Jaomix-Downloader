using System.Diagnostics;

namespace JaomixDownloader
{
    public class TxtToEpubConverter
    {
        private readonly string _os = ConfigGiver.GiveOS();
        public void Convert(string txtFilePath, string epubFilePath)
        {
            switch (_os)
            {
                case "windows":
                {
                    string strCmdText = $"/C pandoc {txtFilePath} -o {epubFilePath}";
                    System.Diagnostics.Process.Start("powershell.exe", strCmdText);
                    break;
                }
                case "linux":
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = "/bin/bash";
                    proc.StartInfo.Arguments = "-c \" " + $"pandoc { txtFilePath} -o { epubFilePath}" + " \"";
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.Start();
                    break;
                }
            }
        }


        public void MakeBook(Book book)
        {
            switch (_os)
            {
                case "windows":
                {
                    string strCmdText = $"/C pandoc {book.BookFileTxt} {book.MetadataFilePath} -o {book.BookFileEpub}";
                    System.Diagnostics.Process.Start("powershell.exe", strCmdText);
                    break;
                }
                case "linux":
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = "/bin/bash";
                    proc.StartInfo.Arguments = "-c \" " + $"pandoc {book.BookFileTxt} {book.MetadataFilePath} -o {book.BookFileEpub}" + " \"";
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.Start();
                    break;
                }
            }
        }
    }
}
