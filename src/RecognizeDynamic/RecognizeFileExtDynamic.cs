using RecognizeFileExtensionBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RecognizeDynamic
{
    public class RecognizeFileExtDynamic: RecognizeFileExt
    {
        public RecognizeFileExtDynamic()
        {
            
            var typeToFound = typeof(IRecognize);
            Assembly a = Assembly.GetExecutingAssembly();
            var types = a.GetTypes()
                .Where(it=>it.IsPublic)
                .Where(it => it.GetInterface(typeToFound.FullName)!=null)
                .ToArray();
            foreach(var t in types)
            {
                recognizes.Add(Activator.CreateInstance(t)as IRecognize);
            }

        }
    }
}
