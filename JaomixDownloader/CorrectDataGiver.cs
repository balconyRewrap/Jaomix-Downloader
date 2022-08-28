using System.Globalization;

namespace JaomixDownloader
{
    internal class CorrectDataGiver
    {
        public static string GiveValidJaomixLink()
        {
            string url = Console.ReadLine();
            while (true)
            {
                if (url.Contains("jaomix.ru"))
                {
                    return url;
                }

                Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("InvalidJaomixUrl", CultureInfo.CurrentCulture));
                url = Console.ReadLine();
                
            }
        }

        public static string GiveValidFolder(string os)
        {
            string activeFolder = Console.ReadLine();
            switch (os)
            {
                case "windows":
                {
                    while (true)
                    {
                        if (activeFolder[^1].ToString() == "\\")
                        {
                            break;
                        }
                        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("invalidFolder", CultureInfo.CurrentCulture));
                        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("configActiveFolderWindows", CultureInfo.CurrentCulture));
                        activeFolder = Console.ReadLine();
                    }
                    break;
                }
                case "linux":
                {
                    while (true)
                    {
                        if (activeFolder[^1].ToString() == "/")
                        {
                            break;
                        }
                        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("invalidFolder", CultureInfo.CurrentCulture));
                        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("configActiveFolderLinux", CultureInfo.CurrentCulture));
                        activeFolder = Console.ReadLine();
                    }
                    break;
                }
            }

            return activeFolder;
        }

        public static string GiveValidTxtFile()
        {
            string txtFile = Console.ReadLine();
            string fileExtension = txtFile[^4..^0];
            while (true)
            {
                if (fileExtension==".txt")
                {
                    return txtFile;
                }

                Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("invalidTxtFile", CultureInfo.CurrentCulture)); 
                txtFile = Console.ReadLine();
                fileExtension = txtFile[^4..^0];
            }
        }
    }
}
