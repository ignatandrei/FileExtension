using RecognizeFileExtensionBL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace RecognizeFileExtensionBL
{
    public abstract class RecognizeFileExt
    {
        protected RecognizeFileExt()
        {
            recognizes = new List<IRecognize>();
        }

        public IEnumerable<Tuple<int, int,string ,string >> DuplicatesBytes()
        {
            var l = recognizes.Count;
            for (int i = 0; i < l-1; i++)
            {
                var compare = recognizes[i];
                for (int j = i+1; j < l; j++)
                {
                    if (recognizes[j].Equals(compare))
                    {
                        string ext1 = string.Join('|', compare.Extension);
                        string ext2 = string.Join('|', recognizes[j].Extension);

                        yield return new Tuple<int, int,string,string>(i, j, ext1,ext2);
                    }
                }
            }
        }
        public IEnumerable<string> AllExtensions()
        {
            return recognizes.SelectMany(it => it.Extension).Distinct();
        }
        public List<IRecognize> recognizes;
        public int NumberRecognizers
        {
            get
            {
                return recognizes.Count();
            }
            
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
            var data = RecognizeFile(fileContent,  recognizes);

            foreach (var item in data)
            {

                foreach (var ext in item.Extension)
                    yield return ext;

            }
        }
        private IEnumerable<IRecognize> RecognizeFile(byte[] fileContent, IEnumerable<IRecognize> recog)
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
            var ret = RecognizeFile(fileContent, recog);
            return ret.Any() ? Recognize.Success : Recognize.Failure;
        }
    }
}
