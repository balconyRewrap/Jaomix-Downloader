﻿namespace JaomixDownloader
{
    public static class MethodsExtensions
    {
        // убираю из главы все символы, на которые будет ругаться винда в названиях файлов
        public static string NormalizeTitle(this string title)
        {
            string[] uChars = { "\\", "|", "/", ":", "*", "?", Convert.ToString('"'), "<", ">" };
            foreach (var uChar in uChars)
            {
                title = title.Replace(uChar, String.Empty);
            }
            return title;
        } 
        public static string NormalizeUrl(this string url)
        {
            url = url.Split("/")[4];
            string[] uChars = { "\\", "|", "/", ":", "*", "?", Convert.ToString('"'), "<", ">","-" };
            foreach (var uChar in uChars)
            { 
                url = url.Replace(uChar, " ");
            }
            return url;
        }
        public static string GiveMainUrlFromDerivative(this string url)
        {
            url = string.Join("/", url.Split("/")[0..4]);
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
}