using System;
using System.Collections.Generic;
using System.Text;

namespace RecognizerPlugin
{
    /// <summary>
    /// changing curentdir when reading files
    /// File.ReadAllLines("offset0.txt")
    /// or ...
    /// var x = Assembly.GetExecutingAssembly().Location;
    /// </summary>
    class CurDir : IDisposable
    {
        string old;
        public CurDir(string newFolder)
        {
            old = Environment.CurrentDirectory;
            Environment.CurrentDirectory = newFolder;
        }
        public void Dispose()
        {
            Environment.CurrentDirectory = old;
            
        }
    }
}
