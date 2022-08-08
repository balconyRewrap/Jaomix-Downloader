using Pandoc;

namespace Jaomix_Parser
{
    internal class TxtToEpubConverter
    {
        public static async void Converter(string txtFilePath, string epubFilePath)

        {
            await PandocInstance.Convert<CommonMarkIn, Epub3Out>(txtFilePath, epubFilePath);
        }
    }
}
