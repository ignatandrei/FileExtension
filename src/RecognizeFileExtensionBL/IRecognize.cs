using System;

namespace RecognizeFileExtensionBL
{
    public interface IRecognize: IEquatable<IRecognize>
    {
        Result InfoNeeded(byte[] b=null);
        string[] Extension { get; set; }
    }
}
