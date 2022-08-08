using System.Configuration;

namespace Jaomix_Parser;

internal class ConfigGiver
{
    public static string Giver(string decision = "0")
    {
        if (decision != "0")
        {
            var currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            Console.WriteLine("Введите путь, в который сохранять все файлы");
            string folderDirectoryReadLine = Console.ReadLine();
            currentConfig.AppSettings.Settings["folder_directory"].Value = folderDirectoryReadLine;
            currentConfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        string folderDirectory = ConfigurationManager.AppSettings.Get("folder_directory");


        if (folderDirectory == null)
        {
            Console.WriteLine("Введите путь, в который сохранять все файлы");
            string FD = Console.ReadLine();
            // открываем текущий конфиг специальным обьектом
            var currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // добавляем позицию в раздел AppSettings
            currentConfig.AppSettings.Settings.Add("folder_directory", FD);
            //сохраняем
            currentConfig.Save(ConfigurationSaveMode.Full);
            //принудительно перезагружаем соотвествующую секцию
            ConfigurationManager.RefreshSection("appSettings");
            folderDirectory = ConfigurationManager.AppSettings.Get("folder_directory");
            return folderDirectory;
        }

        return folderDirectory;
    }
}