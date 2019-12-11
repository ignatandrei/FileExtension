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
            //bytes = new byte[6];
            //bytes[0] = 0x37;
            //bytes[1] = 0x7a;
            //bytes[2] = 188;
            //bytes[3] = 175;
            //bytes[4] = 39;
            //bytes[5] = 28;
            Extension = new string[1] { "7z" };
        }
    }
}
