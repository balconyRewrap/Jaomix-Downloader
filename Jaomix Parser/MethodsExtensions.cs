namespace Jaomix_Parser
{
    public static class MethodsExtensions
    {
        // убираю из главы все символы, на которые будет ругаться винда в названиях файлов
        public static string TitleNormalizer(this string title)
        {
            string[] uChars = { "\\", "|", "/", ":", "*", "?", Convert.ToString('"'), "<", ">" };
            foreach (var uChar in uChars)
            {
                title = title.Replace(uChar, String.Empty);
            }
            return title;
        }
        public static string UrlNormalizer(this string url)
        {
            url = url.Split("/")[4];
            string[] uChars = { "\\", "|", "/", ":", "*", "?", Convert.ToString('"'), "<", ">","-" };
            foreach (var uChar in uChars)
            {
                url = url.Replace(uChar, " ");
            }
            return url;
        }

    }
}
