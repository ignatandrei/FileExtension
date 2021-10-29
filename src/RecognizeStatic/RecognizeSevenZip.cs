using RecognizeFileExtensionBL;
using System;

namespace RecognizeStatic
{
    /// <summary>
    /// taken from https://en.wikipedia.org/wiki/List_of_file_signatures
    /// </summary>
    public class RecognizeSevenZip : RecognizeFirstBytes
    {
        public RecognizeSevenZip():base("377ABCAF271C")
        {
            Extension = new string[1] { "7z" };
        }
    }
}
