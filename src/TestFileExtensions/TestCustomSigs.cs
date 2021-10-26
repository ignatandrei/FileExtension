using FluentAssertions;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using RecognizeCustomSigs_GCK;
using RecognizeFileExtensionBL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace TestFileExtensions
{
    [FeatureDescription(@"test signatures")]
    [Label(nameof(TestCustomSigs))]
    public class TestCustomSigs: FeatureFixture
    {
        RecognizeFileExt r;
        [Scenario]
        [ScenarioCategory("TestSimple")]
        public async void TestCHM1() 
        {
            await Runner
               .AddSteps(Given_The_Recognizer)
               .AddSteps(_ => Then_Should_Recognize_Extension("chm"))
               //.AddAsyncSteps(
               //    _ => When_The_User_Access_The_Url("/recordVisitors/AllVisitors5Min")
               //)
               //.AddSteps(
               //    _ => Then_The_Response_Should_Contain("JeanIrvine")

               //)
               .RunAsync();

        }
        private void Given_The_Recognizer()
        {
            r = new RecognizeCustomSigs();
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
