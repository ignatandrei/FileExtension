using FluentAssertions;
using RecognizeCustomSigs_GCK;
using RecognizeFileExtensionBL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace TestFileExtensions
{
    public class TestCustomSigs
    {
        [Fact]
        public void TestCHM()
        {
            var r = new RecognizeCustomSigs();

            r.CanRecognizeExtension("chm").Should().BeTrue();
            foreach (var item in Directory.EnumerateFiles(@"TestFiles", "*.*"))
            {
                var f = File.ReadAllBytes(item);
                r.RecognizeTheFile(f,item ).Should().Be(Recognize.Success);


            }
        }
    }
}
