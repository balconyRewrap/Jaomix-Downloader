using System.Configuration;
using System.Globalization;

namespace JaomixDownloader;


internal class ConfigGiver
{
    public string GiveActiveFolderPath()
    {
        string folderDirectory = ConfigurationManager.AppSettings.Get("folder_directory");
        if (folderDirectory == null) folderDirectory = CreateActiveFolderPath();
        return folderDirectory;
    }

    private string CreateActiveFolderPath()
    {
        var currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        Console.WriteLine(GlobalStrings.ResourceManager.GetString("configActiveFolder", CultureInfo.CurrentCulture));
        string newActiveFolderDirectory = Console.ReadLine();
        // добавляем позицию в раздел AppSettings
        currentConfig.AppSettings.Settings.Add("folder_directory", newActiveFolderDirectory);
        currentConfig.Save(ConfigurationSaveMode.Full);
        //принудительно перезагружаем соотвествующую секцию
        ConfigurationManager.RefreshSection("appSettings");
        string activeFolderPath = ConfigurationManager.AppSettings.Get("folder_directory");

        return activeFolderPath;
    }

    public void ChangeActiveFolderPath()
    {
        var currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        Console.WriteLine(
            GlobalStrings.ResourceManager.GetString("configActiveFolder", CultureInfo.CurrentCulture));
        string folderDirectoryReadLine = Console.ReadLine();
        // открываем текущий конфиг специальным обьектом
        currentConfig.AppSettings.Settings["folder_directory"].Value = folderDirectoryReadLine;
        currentConfig.Save(ConfigurationSaveMode.Modified);
        //принудительно перезагружаем соотвествующую секцию
        ConfigurationManager.RefreshSection("appSettings");

        Console.WriteLine(ConfigurationManager.AppSettings.Get("folder_directory"));
    }

    public string GiveProgramLanguage()
    {
        string userLanguage = ConfigurationManager.AppSettings.Get("language");


        if (userLanguage == null) userLanguage = CreateProgramLanguage();

        return userLanguage;
    }

    private string CreateProgramLanguage()
    {
        Console.WriteLine(GlobalStrings.ResourceManager.GetString("langGiverHead", CultureInfo.CurrentCulture));
        // стандартное значение
        string lang = "en-US";
        Console.WriteLine("");
        string langchoice = Console.ReadLine();
        switch (langchoice)
        {
            case "1":
                lang = "ru";
                break;
            case "2":
                lang = "en-US";
                break;
        }

        // открываем текущий конфиг специальным обьектом
        var currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        // добавляем позицию в раздел AppSettings
        currentConfig.AppSettings.Settings.Add("language", lang);
        //сохраняем
        currentConfig.Save(ConfigurationSaveMode.Full);
        //принудительно перезагружаем соотвествующую секцию
        ConfigurationManager.RefreshSection("appSettings");
        string userLanguage = ConfigurationManager.AppSettings.Get("language");
        return userLanguage;
    }

    public void ChangeProgramLanguage()
    {
        var currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        Console.WriteLine(
            GlobalStrings.ResourceManager.GetString("langGiverHead", CultureInfo.CurrentCulture));
        string lang = "en-US";
        string langchoice = Console.ReadLine();
        switch (langchoice)
        {
            case "1":
                lang = "ru";
                break;
            case "2":
                lang = "en-US";
                break;
        }

        // открываем текущий конфиг специальным обьектом
        currentConfig.AppSettings.Settings["language"].Value = lang;
        currentConfig.Save(ConfigurationSaveMode.Modified);
        //принудительно перезагружаем соотвествующую секцию
        ConfigurationManager.RefreshSection("appSettings");

        Console.WriteLine(ConfigurationManager.AppSettings.Get("language"));
    }
}