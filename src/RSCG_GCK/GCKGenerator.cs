﻿using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RSCG_GCK
{
    [Generator]
    public class GCKGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            try
            {
                var assembly = context.Compilation;
                var whatToGenerate = assembly.ContainsSymbolsWithName("RecognizePlugins");
                if (whatToGenerate)
                {
                    GenerateIsFunctions_RecognizePlugins(context);
                    return;
                }
                whatToGenerate = assembly.ContainsSymbolsWithName("RecognizeFileController");

                if (whatToGenerate)
                {
                    GenerateIsFunctions_RecognizeController(context);
                    return;
                }
                {
                    GenerateCustomSigGCK(context);
                    GenerateOffset(context);
                }
            }
            catch (Exception ex)
            {
                var severity = DiagnosticSeverity.Error;
                var dd = new DiagnosticDescriptor(DiagnosticId, Title, $"err : {ex.Message}", Category, severity, isEnabledByDefault: true, description: $"err {ex.ToString()}");
                var dg = Diagnostic.Create(dd, Location.None);
                context.ReportDiagnostic(dg);
            }
        }
        private HashSet<string> Extensions()
        {
            var extensions = new HashSet<string>();
            var lines = GetResourceFile("RSCG_GCK.customsigs_GCK.txt");
            List<string> classes = new();
            foreach (var l in lines)
            {
                var data = l.Replace("\r", "").Replace("\n", "").Trim();
                if (data.StartsWith("#"))//comment
                    continue;
                if (string.IsNullOrEmpty(data))
                    continue;
                if (data.Contains("(none)"))
                    continue;
                if (data.Contains("(NONE)"))
                    continue;
                data = data.Replace(".", "_");
                var split = data.Split(',');
                string ext = split[2].Trim();
                foreach (var item in ext.Split('|'))
                {
                    extensions.Add(item.Trim().ToUpper());
                }

            }
            return extensions;
        }
        private void GenerateIsFunctions_RecognizeController(GeneratorExecutionContext context)
        {

            var extensions = Extensions();
            var str = "namespace RecognizeFileExtWebAPI.Controllers{";
            str += Environment.NewLine;
            str += "public partial class RecognizeFileController{";
            str += Environment.NewLine;
            foreach (var item in extensions)
            {
                str += $@"
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async System.Threading.Tasks.Task<RecognizeFileExtensionBL.Recognize>  Is_{item}(Microsoft.AspNetCore.Http.IFormFile file) 
        {{
            var bContent =await  GetFileContents(file);
            if (bContent == null)
                return RecognizeFileExtensionBL.Recognize.GiveMeMoreInfo;
            return IsCorrectExtensionSendByte(""{item}"",bContent);
        }}";
            }
            str += "}";
            str += Environment.NewLine;
            str += "}";
            context.AddSource("RecognizePlugins_Is", str);
        }
        private void GenerateIsFunctions_RecognizePlugins(GeneratorExecutionContext context)
        {

            var extensions = Extensions();
            var str = "namespace RecognizerPlugin{";
            str += Environment.NewLine;
            str += "public partial class RecognizePlugins{";
            str += Environment.NewLine;
            foreach (var item in extensions)
            {
                str += $@"
                public RecognizeFileExtensionBL.Recognize  Is_{item}(byte[] content) 
                {{
                    return RecognizeTheFile(content, ""{item}"");
                }}";
            }
            str += "}";
            str += Environment.NewLine; 
            str += "}";
            context.AddSource("RecognizePlugins_Is", str);
        }
        string[] GetResourceFile(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            //var resourceName = "MyCompany.MyProduct.MyFile.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result.Split('\n');
            }
        }
        void GenerateOffset(GeneratorExecutionContext context)
        {

            var nameSpace = "RecognizeLoadFile";
            var lines = GetResourceFile("RSCG_GCK.offset0.txt");
            List<string> classes = new();
            foreach (var l in lines)
            {
                var data = l.Replace("\r", "").Replace("\n", "").Trim();
                if (data.StartsWith("#"))//comment
                    continue;
                if (string.IsNullOrEmpty(data))
                    continue;
                string className = $"RecognizeFromOffsetLine{classes.Count}";
                string content = $@"
namespace {nameSpace} {{
    class {className}: RecognizeFromLine{{
        public {className}(): base(""{data}"")                
        {{
        }}
    }}
}}            
";
                context.AddSource($"MyGeneratorWiki{classes.Count}.cs", content);
                classes.Add(className);
                //if (classes.Count == 3) break;
                //var r = new RecognizeFromLineCustomsigs(l);
                //recognizes.Add(r);
            }
            var add = string.Join(Environment.NewLine, classes.Select(it => @$"recognizes.Add(new {it}());"));
            context.AddSource("RecognizeFileExtFromFile_recognizes.cs", $@"
namespace {nameSpace}
{{
    partial class RecognizeFileExtFromFile
    {{
        partial void AddRecognizers(){{
                {add}
                }}//end AddRecognizers
    }}//end class
}}//end namespace
            
");

        }
        private static readonly string DiagnosticId = "Gen";
        private static readonly string Title = "Gen";
        private static readonly string Category = "Gen";
        void GenerateCustomSigGCK(GeneratorExecutionContext context)
        {

            var nameSpace = "RecognizeCustomSigs_GCK";
            var lines = GetResourceFile("RSCG_GCK.customsigs_GCK.txt");
            List<string> classes = new();
            foreach (var l in lines)
            {
                var data = l.Replace("\r", "").Replace("\n", "").Trim();
                if (data.StartsWith("#"))//comment
                    continue;
                if (string.IsNullOrEmpty(data))
                    continue;
                if (data.Contains("(none)"))
                    continue;
                if (data.Contains("(NONE)"))
                    continue;
                data = data.Replace(".", "_");
                var split = data.Split(',');
                string ext = split[2].Trim();
                ext = ext.Replace("|", "");
                string className = $"RecognizeFromGCKLine{classes.Count}_{ext}";
                string content = $@"
namespace {nameSpace} {{
    class {className}: RecognizeFromLineCustomsigs{{
        public {className}(): base(""{data}"")                
        {{
        }}
    }}
}}            
";
                context.AddSource($"MyGeneratorGCK{classes.Count}.cs", content);
                classes.Add(className);
                //if (classes.Count == 3) break;
                //var r = new RecognizeFromLineCustomsigs(l);
                //recognizes.Add(r);
            }
            var add = string.Join(Environment.NewLine, classes.Select(it => @$"recognizes.Add(new {it}());"));
            context.AddSource("RecognizeCustomSigs_GCK_recognizes.cs", $@"
namespace RecognizeCustomSigs_GCK
{{
    partial class RecognizeCustomSigs
    {{
        partial void AddRecognizers(){{
                {add}
                }}//end AddRecognizers
    }}//end class
}}//end namespace
            
            
");

        }

        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }
}
