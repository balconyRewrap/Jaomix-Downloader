namespace JaomixDownloader;

public static class MethodsExtensions
{
    public static string NormalizeTitle(this string title)
    {
        string[] uChars = { "\\", "|", "/", ":", "*", "?", Convert.ToString('"'), "<", ">" };
        foreach (string uChar in uChars) title = title.Replace(uChar, string.Empty);
        return title;
    }

    public static string NormalizeUrl(this string url)
    {
        url = url.Split("/")[4];
        string[] uChars = { "\\", "|", "/", ":", "*", "?", Convert.ToString('"'), "<", ">", "-" };
        foreach (string uChar in uChars) url = url.Replace(uChar, " ");
        return url;
    }

    public static string GiveMainUrlFromDerivative(this string url)
    {
        url = string.Join("/", url.Split("/")[..4]);
        return url;
    }

    public static bool IsUselessParagraph(this string paragraph)
    {
        return paragraph.Contains("Jaomix") ||
               paragraph.Contains("Ранобэ, Новеллы на русском, читать онлайн,") ||
               paragraph.Contains("Переводчик") ||
               paragraph.Contains("Если вы обнаружите какие-либо ошибки");
    }
}