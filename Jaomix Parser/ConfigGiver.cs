using System.Configuration;

namespace Jaomix_Parser;
interface IConfigGiver
{
    string Giver();
}

internal class ConfigGiver : IConfigGiver
{

    public string Giver()
    {
        string folderDirectory = ConfigurationManager.AppSettings.Get("folder_directory");


        if (folderDirectory == null)
        {
            Console.WriteLine("Введите путь, в который сохранять все файлы. Например C:\\folder\\");
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
    public void Changer()
    {

        var currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        Console.WriteLine("Введите путь, в который сохранять все файлы. Например C:/folder/");
        string folderDirectoryReadLine = Console.ReadLine();
        // открываем текущий конфиг специальным обьектом
        currentConfig.AppSettings.Settings["folder_directory"].Value = folderDirectoryReadLine;
        currentConfig.Save(ConfigurationSaveMode.Modified);
        //принудительно перезагружаем соотвествующую секцию
        ConfigurationManager.RefreshSection("appSettings");

        Console.WriteLine(ConfigurationManager.AppSettings.Get("folder_directory"));

    }
}