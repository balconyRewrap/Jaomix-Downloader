using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaomix_Parser
{
    internal interface IBookDownloader
    {
        public void MakeBook(string folder, string[] links, string bookFileTxt, string bookFileEpub, string bookName,
            string authorName, int dec, FileMaker fileMaker, string finalFileType);
    }
}
