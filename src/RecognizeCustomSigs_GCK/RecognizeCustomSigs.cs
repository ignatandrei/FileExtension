using RecognizeFileExtensionBL;
using System;
using System.IO;

namespace RecognizeCustomSigs_GCK
{
    public partial class RecognizeCustomSigs: RecognizeFileExt
    {
        partial void AddRecognizers();
        public RecognizeCustomSigs()
        {
            AddRecognizers();

            //var lines = File.ReadAllLines("customsigs_GCK.txt");
            //foreach (var l in lines)
            //{
            //    var data = l.Replace("\r", "").Replace("\n", "").Trim();
            //    if (data.StartsWith("#"))//comment
            //        continue;
            //    if (string.IsNullOrEmpty(data))
            //        continue;
            //    var r = new RecognizeFromLineCustomsigs(l);
            //    recognizes.Add(r);
            //}
        }
        
    }
}
