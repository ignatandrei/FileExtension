
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var r = new RecognizerPlugin.RecognizePlugins();
            //foreach (var item in r.AllExtensions())
            //{
            //    Console.WriteLine(item);
            //}
            //find the sln on the path
            string file = FindSlnToBeRecognized();
            //found sln, now recognize
            var byts = await File.ReadAllBytesAsync(file);

            //find short way - directly
            var foundDirectly = r.Is_SLN(byts);
            Console.WriteLine($"file {file} is recognized {foundDirectly}");
            // or long way
            //find extension
            var fileExtension = Path.GetExtension(file);
            // find if there is a recognizer for the extension
            var canRecognize = r.CanRecognizeExtension(fileExtension);
            Console.WriteLine($"file {file} can be  recognized {canRecognize}");
            if (canRecognize)
            {
                //now see if the content matches the bytes
                var found = r.RecognizeTheFile(byts, fileExtension);
                Console.Write($"file {file} is recognized {found}");
            }
            
            return;
        }
        static string FindSlnToBeRecognized()
        {
            var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (dir != null)
            {
                string file = Directory.GetFiles(dir.FullName, "*.sln").FirstOrDefault();
                if (file != null)
                {
                    return file;
                }
                dir = dir.Parent;

            }
            return null;
        }
    }
}
