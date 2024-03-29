﻿using FluentAssertions;
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
               .AddSteps(_ => Then_Should_Not_Recognize_Extension("alabalaportocala"))
            .RunAsync();

        }

        [Scenario]
        [ScenarioCategory("TestSimple")]
        public async void TestPossibleExtensionFOrDocx()
        {
            await Runner
               .AddSteps(Given_The_Recognizer)
               .AddAsyncSteps(_ => Then_ShouldRecognize_Multiple_Extension("TestFiles/andrei.docx"))
            .RunAsync();

        }

        private async Task Then_ShouldRecognize_Multiple_Extension(string nameFile)
        {
             
            var bytes = await File.ReadAllBytesAsync(nameFile);
            var arr = r.PossibleExtensions(bytes);
            arr.Should().HaveCountGreaterThanOrEqualTo(2);//word and zip
            foreach (var item in arr)
            {
                StepExecution.Current.Comment($"{item}");
            }
        }
        
        [Scenario]
        [ScenarioCategory("Data")]
        public async void EnumerateRecognizers()
        {
            await Runner
               .AddSteps(Given_The_Recognizer)
               .AddSteps(
                    _ => Then_Extensions_Tested_Are_More_Than(16),
                    _ => Then_Extensions_That_Can_Be_Recognized_Are_More_Than(300)                    
               )
           .RunAsync();

        }
        private void Then_Extensions_Tested_Are_More_Than(int number)
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
            extensions.Should().HaveCountGreaterThanOrEqualTo(number);
            StepExecution.Current.Comment($"Number of files recognized:{ extensions.Length} ");
            foreach (var item in extensions)
            {
                StepExecution.Current.Comment($"{item}");                
            }
        }

        private void Then_Extensions_That_Can_Be_Recognized_Are_More_Than(int number)
        {


            //var recog = r.recognizes;
            var AllExtensions= r
                .AllExtensions()
                .ToArray();

            AllExtensions.Should().HaveCountGreaterThanOrEqualTo(number);
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
            r.RecognizeTheFile(bytesFile, file).Should().Be(Recognize.Success,$"not recognize {file}");
        }
        private void Then_Should_Recognize_Extension(string ext)
        {
            r.CanRecognizeExtension(ext).Should().BeTrue();
        }
        private void Then_Should_Not_Recognize_Extension(string ext)
        {
            r.CanRecognizeExtension(ext).Should().BeFalse();
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
