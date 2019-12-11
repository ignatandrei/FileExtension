using RecognizeFileExtensionBL;

namespace VariousStarts
{
    class RecognizeFromLine: RecognizeFirstBytes
    {
        public RecognizeFromLine(string line)
        {
            var l = line.Split(',');
            bytes = FromString(l[0].Replace(" ", ""));
            Extension =new string[1] { l[1].Trim() };
        }

        
    }
}
