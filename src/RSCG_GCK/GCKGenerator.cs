using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RSCG_GCK
{
    [Generator]
    public class GCKGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {

            var nameSpace = "RecognizeCustomSigs_GCK";
            var lines = File.ReadAllLines("customsigs_GCK.txt");
            List<string> classes = new ();
            foreach (var l in lines)
            {
                var data = l.Replace("\r", "").Replace("\n", "").Trim();
                if (data.StartsWith("#"))//comment
                    continue;
                if (string.IsNullOrEmpty(data))
                    continue;
                var split = data.Split(',');
                string ext = split[2].Trim();
                ext = ext.Replace("|", "");
                string className = $"RecognizeFromLine{ext}";
                string content = $@"
namespace {nameSpace} {{
    public class {className}: RecognizeFromLineCustomsigs{{
        public {className}(""{data}""): base(line)                

    }}
}}            
";
                context.AddSource($"generator{classes.Count}.cs",content);
                classes.Add(className);
                if (classes.Count == 2)
                    break;
                //var r = new RecognizeFromLineCustomsigs(l);
                //recognizes.Add(r);
            }
            var add = string.Join(Environment.NewLine, classes.Select(it => @$"recognizes.Add(""{it}"");"));
            context.AddSource("RecognizeCustomSigs_GCK_recognizes.cs", $@"
namespace RecognizeCustomSigs_GCK
{{
    public partial class RecognizeCustomSigs
    {{
        void AddRecognizers(){{
                {add}
                }}
            
");

        }

        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }
}
