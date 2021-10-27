using FluentAssertions;
using RecognizeCustomSigs_GCK;
using RecognizeDynamic;
using RecognizeFileExtensionBL;
using RecognizeLoadFile;
using RecognizeStatic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace TestFileExtensions
{
    public class RecognizersFile :  IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            return MyEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return MyEnumerable().GetEnumerator();
        }

        private IEnumerable<object[]> MyEnumerable()
        {
            yield return new[] { new RecognizeFileExtStatic() };
            yield return new[] { new RecognizeFileExtDynamic() };
            yield return new[] { new RecognizeFileExtFromFile() };
            yield return new[] { new RecognizeCustomSigs() };
        }
    }

        public class TestFolder
    {
        //[Theory]
        //[ClassData(typeof(RecognizersFile))]
        //public void TestContentFile(RecognizeFileExt r)
        //{
        //    r.NumberRecognizers.Should().BeGreaterThan(0, "have some recognizers");
        //    //Assert.True(r.NumberRecognizers > 0);
        //    foreach (var item in Directory.EnumerateFiles(@"TestFiles","*.*",SearchOption.TopDirectoryOnly))
        //    {
        //        var ext = Path.GetExtension(item);
        //        var s = r.PossibleExtensions(File.ReadAllBytes(item));
        //        if (r.CanRecognizeExtension(ext))
        //        {
        //            s.Should().HaveCountGreaterThan(0, $"for {r.GetType().Name} extension {ext} recognized , but not found in possible extension");
        //            //Assert.True(s.Any());
        //        }
        //        else
        //        {
        //            s.Should().BeEmpty($"for {r.GetType().Name} extension {ext} recognized , but not found in possible extension");
        //            //Assert.False(s.Any());
        //        }
        //    }
        //}
        [Theory]
        [ClassData(typeof(RecognizersFile))]
        public void TestDuplicates(RecognizeFileExt r)
        {
            //This should have tostring to recognize
            r.DuplicatesBytes().Should().BeEmpty($"{r.GetType().Name} have duplicates");
            //Assert.Empty(r.Duplicates());
        }
        
        [Theory]
        [ClassData(typeof(RecognizersFile))]
        public void TestFilesToRecognize(RecognizeFileExt r)
        {
            
            foreach (var item in Directory.EnumerateFiles(@"TestFiles", "*.*", SearchOption.TopDirectoryOnly))
            {
                var ext = Path.GetExtension(item);
                var s = r.RecognizeTheFile(File.ReadAllBytes(item), ext);
                if (r.CanRecognizeExtension(ext))
                {
                    Assert.Equal(Recognize.Success, s);
                }
                else
                {
                    Assert.Equal(Recognize.Failure, s);
                }
            }
        }

    }
}
