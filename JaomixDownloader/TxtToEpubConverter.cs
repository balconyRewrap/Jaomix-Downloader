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


        public void MakeBook(BookFile bookFile)
        {
            switch (_os)
            {
                case "windows":
                {
                    string strCmdText = $"/C pandoc {bookFile.BookFileTxt} {bookFile.MetadataFilePath} -o {bookFile.BookFileEpub}";
                    Process.Start("powershell.exe", strCmdText);
                    break;
                }
                case "linux":
                {
                    var proc = new Process();
                    proc.StartInfo.FileName = "/bin/bash";
                    proc.StartInfo.Arguments = "-c \" " + $"pandoc {bookFile.BookFileTxt} {bookFile.MetadataFilePath} -o {bookFile.BookFileEpub}" + " \"";
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.Start();
                    break;
                }
            }
        }
    }
}
