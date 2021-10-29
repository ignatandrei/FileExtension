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
static void Main(string[] args)
{
    Console.WriteLine("Hello World!");
    var r = new RecognizerPlugin.RecognizePlugins();
    foreach (var item in r.AllExtensions())
    {
        Console.WriteLine(item);
    }
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

Also, thanks to the people that made this possible by created frameworks and libraries - see the list here

https://github.com/ignatandrei/RecordVisitors/blob/main/src/RecordVisitors/thanks.md

## Links

- Project homepage: https://github.com/ignatandrei/FileExtension/ 
- Code Coverage: https://codecov.io/gh/ignatandrei/FileExtension 
- Results of tests in BDD format : https://record-visitors.readthedocs.io/en/latest/BDD/LightBDDReport/ 
- Code analysis: https://sonarcloud.io/dashboard?id=ignatandrei_RecordVisitors
- Repository: https://github.com/ignatandrei/FileExtension/
- Issue tracker: https://github.com/ignatandrei/RecordVisitors/issues
- Documentation: https://record-visitors.readthedocs.io/en/latest/ 
- Blog: http://msprogrammer.serviciipeweb.ro/category/FileExtension/ 

## Licence

The code in this project is licensed under MIT license.
You can find the licences for the packages used at https://github.com/ignatandrei/RecordVisitors/blob/main/src/RecordVisitors/licenses.txt 