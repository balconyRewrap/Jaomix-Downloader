using System.Diagnostics;
using JaomixDownloader.ClassesForParameters;
using JaomixDownloader.Resources;

namespace JaomixDownloader;

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
                Process.Start("powershell.exe", strCmdText);
                Console.WriteLine(GlobalResources.convertingWait);
                while (!File.Exists(epubFilePath))
                {
                }

                break;
            }
            case "linux":
            {
                var proc = new Process();
                proc.StartInfo.FileName = "/bin/bash";
                proc.StartInfo.Arguments = "-c \" " + $"pandoc {txtFilePath} -o {epubFilePath}" + " \"";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();

                Console.WriteLine(GlobalResources.convertingWait);
                while (!File.Exists(epubFilePath))
                {
                }

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
                string strCmdText =
                    $"/C pandoc {bookFile.BookFileTxt} {bookFile.MetadataFilePath} -o {bookFile.BookFileEpub}";
                Console.WriteLine(strCmdText);
                Process.Start("powershell.exe", strCmdText);

                Console.WriteLine(GlobalResources.convertingWait);
                while (!File.Exists(bookFile.BookFileEpub))
                {
                }

                break;
            }
            case "linux":
            {
                var proc = new Process();
                proc.StartInfo.FileName = "/bin/bash";
                proc.StartInfo.Arguments = "-c \" " +
                                           $"pandoc {bookFile.BookFileTxt} {bookFile.MetadataFilePath} -o {bookFile.BookFileEpub}" +
                                           " \"";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();
                Console.WriteLine(GlobalResources.convertingWait);
                while (!File.Exists(bookFile.BookFileEpub))
                {
                }

                break;
            }
        }
    }
}