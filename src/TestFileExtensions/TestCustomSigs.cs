using FluentAssertions;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using RecognizeCustomSigs_GCK;
using RecognizeFileExtensionBL;
using RecognizerPlugin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestFileExtensions
{
    public class DirectoryTestData :  IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            return GetData();
        }

        IEnumerator<object[]> GetData()
        {
            foreach (var item in Directory.EnumerateFiles(@"TestFiles", "*.*"))
            {
                yield return new object[] { item };
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    [FeatureDescription(@"test signatures")]
    [Label(nameof(TestCustomSigs))]
    public class TestCustomSigs: FeatureFixture
    {
        RecognizeFileExt r;
        string fileName;
        byte[] bytesFile;
        [Scenario]
        [ScenarioCategory("TestSimple")]
        public async void TestCanRecognizeCHM() 
        {
            await Runner
                
               .AddSteps(Given_The_Recognizer)
               .AddSteps(_ => Then_Should_Recognize_Extension("chm"))               
               .RunAsync();

        }
        [Scenario]
        [ScenarioCategory("TestSimple")]
        [ClassData(typeof(DirectoryTestData))]
        public async void TestMultipleFiles(string nameFile)
        {
                fileName = nameFile;
                await Runner
               .AddSteps(Given_The_Recognizer)
               .AddAsyncSteps(_ => When_Read_The_File(fileName))
               .AddSteps(
                    _ => Then_Should_Recognize_File(fileName),
                    _ => Then_The_Extension_Matches(fileName)

               )
               .RunAsync();
            
        }
        private async Task When_Read_The_File(string file)
        {
            StepExecution.Current.Comment($"recognizing:{Path.GetExtension(file)}");
            bytesFile = await File.ReadAllBytesAsync(file);

        }
        private void Given_The_Recognizer()
        {
            r = new RecognizePlugins();
        }
        private void Then_The_Extension_Matches(string file)
        {
            var ext = Path.GetExtension(file);
            var s = r.PossibleExtensions(bytesFile);
            if (r.CanRecognizeExtension(ext))
            {
                s.Should().HaveCountGreaterThan(0, $"for {r.GetType().Name} extension {ext} recognized , but not found in possible extension");
                //Assert.True(s.Any());
            }
            else
            {
                s.Should().BeEmpty($"for {r.GetType().Name} extension {ext} recognized , but not found in possible extension");
                //Assert.False(s.Any());
            }
        }
        private void Then_Should_Recognize_File(string file)
        {
            r.RecognizeTheFile(bytesFile, file).Should().Be(Recognize.Success);
        }
        private void Then_Should_Recognize_Extension(string ext)
        {
            r.CanRecognizeExtension("chm").Should().BeTrue();
        }

        [Fact]
        public void TestAll()
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
