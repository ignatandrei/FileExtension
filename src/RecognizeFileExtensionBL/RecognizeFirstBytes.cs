using System;
using System.Globalization;
using System.Linq;

namespace RecognizeFileExtensionBL
{
    public abstract class RecognizeFirstBytes : IRecognize
    {
        public RecognizeFirstBytes()
        {

        }
        public RecognizeFirstBytes(string hex)
        {
            bytes = FromString(hex);
        }
        protected byte[] FromString(string hex)
        {
            if (hex.Length % 2 != 0)
                throw new ArgumentException($"cannot convert {hex} to byte");

            var l = hex.Length / 2;
            var arr = new byte[l];
            for (int i = 0; i < l; i++)
            {
                arr[i]=byte.Parse(hex.Substring(i*2,2),NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            return arr;
        }
        public byte[] bytes;

        public string[] Extension { get; set; }

        public Result InfoNeeded(byte[] b = null)
        {
            var res = new Result();
            if (b == null)
            {
                res.Recognize = Recognize.GiveMeMoreInfo;
                res.GiveMeMore = new GiveMeMoreBytes
                {
                    StartByte = 0,
                    EndByte = bytes.Length
                };
                return res;

            }
            if (b.Length < bytes.Length)
            {
                res.Recognize = Recognize.Failure;
                return res;
            }
            for (int i = 0; i < bytes.Length; i++)
            {
                if(b[i] != bytes[i])
                {
                    res.Recognize = Recognize.Failure;
                    return res;
                }
            }

            res.Recognize = Recognize.Success;
            
            return res;

        }

        public bool Equals(IRecognize other)
        {
            if (other == null)
                return false;

            if (!(other is RecognizeFirstBytes fb))
                return false;

            if (!this.bytes.SequenceEqual(fb.bytes))
                return false;

            return true;
        }
        /// <summary>
        /// used for r.Duplicates().Should().BeEmpty
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.GetType().Name + "-" + BitConverter.ToString(this.bytes);
        }
    }
}
