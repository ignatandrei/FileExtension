using RecognizeFileExtensionBL;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecognizeDynamic
{
    /// <summary>
    /// taken from https://en.wikipedia.org/wiki/List_of_file_signatures
    /// </summary>
    public class RecognizePDF : RecognizeFirstBytes
    {
        public RecognizePDF():base("25504446")
        {
            Extension = new string[1] { "PDF"};
        }
    }
}
