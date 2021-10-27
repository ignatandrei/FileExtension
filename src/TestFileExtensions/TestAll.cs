using FluentAssertions;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using RecognizeCustomSigs_GCK;
using RecognizeFileExtensionBL;
using RecognizerPlugin;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestFileExtensions
{
    [FeatureDescription(@"test signatures")]
    [Label(nameof(TestAll))]
    public class TestAll: FeatureFixture
    {
        RecognizeFileExt r;
        string fileName;
        byte[] bytesFile;
        string[] extensions;
        [Scenario]
        [ScenarioCategory("TestSimple")]
        public async void TestCanRecognizeSome() 
        {
            await Runner                
               .AddSteps(Given_The_Recognizer)
               .AddSteps(_ => Then_Should_Recognize_Extension("chm"))
               .AddSteps(_ => Then_Should_Recognize_Extension("doc"))
               .AddSteps(_ => Then_Should_Recognize_Extension("epub"))
            .RunAsync();

        }
        [Scenario]
        [ScenarioCategory("Data")]
        public async void EnumerateRecognizers()
        {
            await Runner
               .AddSteps(Given_The_Recognizer)
               .AddSteps(
                    _ => Then_Enumerate_Extensions_Recognized(),
                    _ => Then_Enumerate_Extensions_Can_Be_Recognized()
                    
               )
           .RunAsync();

        }
        private void Then_Enumerate_Extensions_Recognized()
        {
            extensions = new DirectoryTestData().ToArray()
                .Select(it=> Path.GetExtension(it.First().ToString()))
                .Where(it=>it.StartsWith("."))
                .Select(it=>it.Substring(1))
                .Select(it => it.ToLowerInvariant())
                .OrderBy(it=>it)
                .Distinct()
                .ToArray()
                ;
            StepExecution.Current.Comment($"Number of files recognized:{ extensions.Length} ");
            foreach (var item in extensions)
            {
                StepExecution.Current.Comment($"{item}");                
            }
        }

        private void Then_Enumerate_Extensions_Can_Be_Recognized()
        {
            //var recog = r.recognizes;
            var AllExtensions= r
                .AllExtensions()
                .Select(it=>it.ToLowerInvariant())
                .OrderBy(it => it)
                .Distinct()
                .ToArray(); 
            StepExecution.Current.Comment($"Number of extensions :{ AllExtensions.Length} ");
            StepExecution.Current.Comment($"Percentage :{ (extensions.Length * 100 / AllExtensions.Length).ToString("#.00")} % ");
            foreach (var item in AllExtensions)
            {
                string text = $"{item} {(extensions.Contains(item) ? ":Tested" : "Not Tested")}";
                StepExecution.Current.Comment(text);

            }

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
            var s = r.PossibleExtensions(bytesFile)
                .Select(it=>it.ToUpper())
                .ToArray();
            if (r.CanRecognizeExtension(ext))
            {
                s.Should().HaveCountGreaterThan(0, $"for {r.GetType().Name} extension {ext} recognized , but not found in possible extension");
                //Assert.True(s.Any());
                ext = ext.Replace(".", "").ToUpper();
                s.Should().Contain(ext, $"{ext} must have been recognized properly");
            }
            else
            {
                s.Should().BeEmpty($"for {r.GetType().Name} extension {ext} recognized , but not found in possible extension");
                //Assert.False(s.Any());
            }
        }
        private void Then_Should_Recognize_File(string file)
        {
            if (file.EndsWith("epub"))
            {
                System.Diagnostics.Debugger.Break();
            }
            r.RecognizeTheFile(bytesFile, file).Should().Be(Recognize.Success);
        }
        private void Then_Should_Recognize_Extension(string ext)
        {
            r.CanRecognizeExtension("chm").Should().BeTrue();
        }

        //[Fact]
        //public void TestAllFilesInFolder()
        //{
        //    var r = new RecognizeCustomSigs();

        //    r.CanRecognizeExtension("chm").Should().BeTrue();
        //    foreach (var item in Directory.EnumerateFiles(@"TestFiles", "*.*"))
        //    {
        //        var f = File.ReadAllBytes(item);
        //        r.RecognizeTheFile(f,item ).Should().Be(Recognize.Success,$"cannot recognize {item}");


        //    }
        //}
    }
}
