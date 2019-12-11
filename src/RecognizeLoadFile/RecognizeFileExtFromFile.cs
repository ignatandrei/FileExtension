using RecognizeFileExtensionBL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace RecognizeLoadFile
{
    public class RecognizeFileExtFromFile : RecognizeFileExt
    {
        public RecognizeFileExtFromFile()
        {
            //var x = Assembly.GetExecutingAssembly().Location;

            
            var lines = File.ReadAllLines("offset0.txt");
            foreach (var l in lines)
            {
                var data = l.Replace("\r", "").Replace("\n", "").Trim();
                if (data.StartsWith("#"))//comment
                    continue;
                var r = new RecognizeFromLine(l);
                recognizes.Add(r);
            }
        }
    }
}
