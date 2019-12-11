using RecognizeFileExtensionBL;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecognizeStatic
{
    public class RecognizeFileExtStatic: RecognizeFileExt
    {
        public RecognizeFileExtStatic()
        {
            recognizes = new List<IRecognize>()
            {
                new RecognizeSevenZip(),
                //put other recognizers here

            };
        }
    }
}
