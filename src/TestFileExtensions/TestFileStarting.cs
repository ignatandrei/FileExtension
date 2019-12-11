using RecognizeDynamic;
using RecognizeFileExtensionBL;
using RecognizeStatic;
using System;
using System.IO;
using Xunit;

namespace TestFileExtensions
{
    public class TestFileStarting
    {
        [Fact]
        public void TestPDF()
        {
            var r = new RecognizePDF();
            foreach (var item in Directory.EnumerateFiles(@"TestFiles", "*.pdf"))
            {
                var f = File.ReadAllBytes(item);
                var q = r.InfoNeeded(null);
                var bytes = f[q.GiveMeMore.StartByte..q.GiveMeMore.EndByte];
                q = r.InfoNeeded(bytes);

                Assert.Equal(Recognize.Success, q.Recognize);
            }
        }
        [Fact]
        public void TestSevenZip()
        {
            var r = new RecognizeSevenZip();
            foreach(var item in Directory.EnumerateFiles(@"TestFiles", "*.7z"))
            {
                var f = File.ReadAllBytes(item);
                var q = r.InfoNeeded(null);
                var bytes = f[q.GiveMeMore.StartByte..q.GiveMeMore.EndByte];
                q = r.InfoNeeded(bytes);
                Assert.Equal(Recognize.Success, q.Recognize);

            }
        }
    }
}
