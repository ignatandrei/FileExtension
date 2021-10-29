using RecognizeFileExtensionBL;

namespace RecognizeStatic
{
    public class RecognizeSln : RecognizeFirstBytes
    {
        public RecognizeSln() : base("EFBBBF0D0A4D6963726F736F66742056697375616C2053747564696F20536F6C7574696F6E2046696C65")
        {
            Extension = new string[1] { "sln" };
        }
    }
}
