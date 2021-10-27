using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

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
}
