# FileExtension
Recognizers of file extensions
[![codecov](https://codecov.io/gh/ignatandrei/FileExtension/branch/master/graph/badge.svg?token=UA3ZA1KDQ5)](https://codecov.io/gh/ignatandrei/FileExtension)
[![Nuget](https://img.shields.io/nuget/v/FileExtension)](https://www.nuget.org/packages/FileExtension/) 
[![.NET](https://github.com/ignatandrei/FileExtension/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/ignatandrei/FileExtension/actions/workflows/dotnet.yml)
[![Docs](https://readthedocs.org/projects/fileextension/badge/?version=latest)](https://fileextension.readthedocs.io/en/latest/)

# What it does

This project helps you to see that a file has the correct extension

# What it recognize

There are 344 extensions of files ( 16 tested) .
See https://fileextension.readthedocs.io/en/latest/BDD/LightBDDReport/

Please help by making a PR by adding files to  https://github.com/ignatandrei/FileExtension/tree/master/src/TestFileExtensions/TestFiles

 
# How to use it

## Simple use

Add a reference to [![Nuget](https://img.shields.io/nuget/v/FileExtension)](https://www.nuget.org/packages/FileExtension/) ,
 
```csharp
Console.WriteLine("Hello World!");
var r = new RecognizerPlugin.RecognizePlugins();
foreach (var item in r.AllExtensions())
{
    Console.WriteLine(item);
}
//find the sln
var dir =new DirectoryInfo( Directory.GetCurrentDirectory());
while(dir != null){
    string file = Directory.GetFiles(dir.FullName, "*.sln").FirstOrDefault();
    if(file == null)
    {
        dir = dir.Parent;
        continue;
    }
    var fileExtension = Path.GetExtension(file);
    var canRecognize = r.CanRecognizeExtension(fileExtension);
    Console.WriteLine($"file {file} can be  recognized {canRecognize}");
    //found sln, now recognize
    var byts = await File.ReadAllBytesAsync(file);
    var found = r.RecognizeTheFile(byts, fileExtension);
    Console.Write($"file {file} is recognized {found}");
    return;
}

```


## Contributors ✨

Thanks goes to these wonderful people ([emoji key](https://allcontributors.org/docs/en/emoji-key)):

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore-start -->
<!-- markdownlint-disable -->
<table>
  <tr>
    <td align="center"><a href="http://msprogrammer.serviciipeweb.ro/"><img src="https://avatars.githubusercontent.com/u/153982?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Andrei Ignat</b></sub></a><br /><a href="https://github.com/ignatandrei/RecordVisitors/commits?author=ignatandrei" title="Tests">⚠️</a> <a href="https://github.com/ignatandrei/RecordVisitors/commits?author=ignatandrei" title="Code">💻</a></td>
  </tr>
</table>

<!-- markdownlint-restore -->
<!-- prettier-ignore-end -->

<!-- ALL-CONTRIBUTORS-LIST:END -->

This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!
## Links

- Project homepage: https://github.com/ignatandrei/FileExtension/ 
- Code Coverage: https://codecov.io/gh/ignatandrei/FileExtension 
- Results of tests in BDD format : https://record-visitors.readthedocs.io/en/latest/BDD/LightBDDReport/ 
- Code analysis: https://sonarcloud.io/dashboard?id=ignatandrei_RecordVisitors
- Repository: https://github.com/ignatandrei/FileExtension/
- Issue tracker: https://github.com/ignatandrei/FileExtension/issues
- Documentation: https://fileextension.readthedocs.io/en/latest/ 
- Blog: http://msprogrammer.serviciipeweb.ro/category/FileExtension/ 

## Licence

The code in this project is licensed under MIT license.
<!-- You can find the licences for the packages used at https://github.com/ignatandrei/FileExtension/blob/main/src/RecordVisitors/licenses.txt  -->