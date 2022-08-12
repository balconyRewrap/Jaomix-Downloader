namespace Jaomix_Parser
{
    public class TxtToEpubConverter
    {
        public static void Converter(string txtFilePath, string epubFilePath)

        {
            string strCmdText;
            strCmdText = $"/C pandoc {txtFilePath} -o {epubFilePath}";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }
    }
}
