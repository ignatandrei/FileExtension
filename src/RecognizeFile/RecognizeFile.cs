using RecognizeFileExtensionBL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VariousStarts;

namespace RecognizeFile
{
    public class RecognizeFileExt
    {
        List<IRecognize> recognizes;
        public RecognizeFileExt()
        {
            recognizes = new List<IRecognize>()
            {
                new RecognizeSevenZip(),
                new RecognizePDF(),
                //new RecognizePNG()
            };
        }
        public bool CanRecognizeExtension(string extension)
        {
            return Recognizers(extension).Any();
        }
        private IEnumerable<IRecognize> Recognizers(string extension)
        {
            extension = extension.ToLowerInvariant();
            var dot = extension.IndexOf(".");
            if (dot > -1)
            {
                extension = extension.Substring(dot + 1);
            }
            


            return recognizes.Where(it =>
                it.Extension.Contains(
                    extension, StringComparer.InvariantCultureIgnoreCase));

        }
        public IEnumerable<string> PossibleExtensions(byte[] fileContent)
        {
            var data = recognizeFile(fileContent,  recognizes);

            foreach (var item in data)
            {

                foreach (var ext in item.Extension)
                    yield return ext;

            }
        }
        private IEnumerable<IRecognize> recognizeFile(byte[] fileContent,  IEnumerable<IRecognize> recog)
        {
            foreach (var item in recog)
            {
                var q = item.InfoNeeded(null);
                while (!q.HaveResult)
                {
                    if (fileContent.Length < q.GiveMeMore.EndByte)
                    {
                        q.Recognize = Recognize.Failure;
                        break;
                    }

                    var bytes = fileContent[q.GiveMeMore.StartByte..q.GiveMeMore.EndByte];
                    q = item.InfoNeeded(bytes);
                }

                if (q.Recognize == Recognize.Success)
                    yield return item;
            }

            

        }
        public Recognize RecognizeTheFile(byte[] fileContent,string extensionOrFileName)
        {
            var recog = Recognizers(extensionOrFileName);
            var ret = recognizeFile(fileContent, recog);
            return ret.Any() ? Recognize.Success : Recognize.Failure;
        }
    }
}
