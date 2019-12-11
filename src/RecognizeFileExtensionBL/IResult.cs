namespace RecognizeFileExtensionBL
{
    public class Result
    {
        public Recognize Recognize { get; set; }
        public GiveMeMoreBytes GiveMeMore { get; set; }

        public bool HaveResult => Recognize
                           != Recognize.GiveMeMoreInfo;
    }
    
}
