
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
            Console.WriteLine("Hello World!");
            var r = new RecognizerPlugin.RecognizePlugins();
            foreach (var item in r.AllExtensions())
            {
                Console.WriteLine(item);
            }
            //find the sln
            string file = FindSlnToBeRecognized();
            var fileExtension = Path.GetExtension(file);
            var canRecognize = r.CanRecognizeExtension(fileExtension);
            Console.WriteLine($"file {file} can be  recognized {canRecognize}");
            //found sln, now recognize
            var byts = await File.ReadAllBytesAsync(file);
            var found = r.RecognizeTheFile(byts, fileExtension);
            Console.Write($"file {file} is recognized {found}");
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
