using System.Configuration;
using System.Globalization;
#pragma warning disable CS8600

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
        string os = GiveOS();
        switch (os)
        {
            case "windows":
                Console.WriteLine(
                Resources.GlobalResources.ResourceManager.GetString("configActiveFolderWindows", CultureInfo.CurrentCulture));
                break;
            case "linux":
                Console.WriteLine(
                    Resources.GlobalResources.ResourceManager.GetString("configActiveFolderLinux", CultureInfo.CurrentCulture));
                break;
        }
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
        string os = GiveOS();
        switch (os)
        {
            case "windows":
                Console.WriteLine(
                    Resources.GlobalResources.ResourceManager.GetString("configActiveFolderWindows", CultureInfo.CurrentCulture));
                break;
            case "linux":
                Console.WriteLine(
                    Resources.GlobalResources.ResourceManager.GetString("configActiveFolderLinux", CultureInfo.CurrentCulture));
                break;
        }
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
        string userLanguage = ConfigurationManager.AppSettings.Get("language") ?? CreateProgramLanguage();

        return userLanguage;
    }

    private string CreateProgramLanguage()
    {
        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("langGiverHead", CultureInfo.CurrentCulture));
        // стандартное значение
        string lang = "en-US";
        Console.WriteLine("");
        string langChoice = Console.ReadLine();
        switch (langChoice)
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
            Resources.GlobalResources.ResourceManager.GetString("langGiverHead", CultureInfo.CurrentCulture));
        string lang = "en-US";
        string langСhoice = Console.ReadLine();
        switch (langСhoice)
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
    public static string GiveOS()
    {
        string os = ConfigurationManager.AppSettings.Get("operatingSystem") ?? CreateOS();
        return os;
    }
    private static string CreateOS()
    {
        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("OSGiverHead", CultureInfo.CurrentCulture));
        // стандартное значение
        string os = "windows";
        Console.WriteLine("");
        string osChoice = Console.ReadLine();
        if (osChoice == "1")
        {
            os = "linux";
        }

        // открываем текущий конфиг специальным обьектом
        var currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        // добавляем позицию в раздел AppSettings
        currentConfig.AppSettings.Settings.Add("operatingSystem", os);
        //сохраняем
        currentConfig.Save(ConfigurationSaveMode.Full);
        //принудительно перезагружаем соотвествующую секцию
        ConfigurationManager.RefreshSection("appSettings");
        string userOS = ConfigurationManager.AppSettings.Get("operatingSystem");
        return userOS;
    }
    public void ChangeOS()
    {
        Console.WriteLine(Resources.GlobalResources.ResourceManager.GetString("OSGiverHead", CultureInfo.CurrentCulture));
        // стандартное значение
        string os = "windows";
        Console.WriteLine("");
        string osChoice = Console.ReadLine();
        if (osChoice == "1")
        {
            os = "linux";
        }

        // открываем текущий конфиг специальным обьектом
        var currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        currentConfig.AppSettings.Settings["operatingSystem"].Value = os;
        currentConfig.Save(ConfigurationSaveMode.Modified);
        //принудительно перезагружаем соотвествующую секцию
        ConfigurationManager.RefreshSection("appSettings");

        Console.WriteLine(ConfigurationManager.AppSettings.Get("operatingSystem"));
    }
}