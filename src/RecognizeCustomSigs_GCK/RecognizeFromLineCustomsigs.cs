using RecognizeFileExtensionBL;

namespace RecognizeCustomSigs_GCK
{
    class RecognizeFromLineCustomsigs : RecognizeFirstBytes
    {
        public RecognizeFromLineCustomsigs(string line)
        {
            var l = line.Split(',');
            bytes = FromString(l[1].Replace(" ", ""));
            string ext = l[2].Trim();
            Extension = ext.Split("|");
        }


    }
}
