using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using prog_buddy_api.Models.Compilation;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace prog_buddy_api.Services
{
    public class CompilerService
    {
        public HashSet<PortableExecutableReference> References { get; set; } = new HashSet<PortableExecutableReference>();


        public CompilerService()
        {
            AddNetCoreDefaultReferences();
        }

        public CompilationResult CompileSource(string code)
        {
            CSharpCompilation compilation = CSharpCompilation.Create("DynamicCode")
                                   .WithOptions(new CSharpCompilationOptions(OutputKind.ConsoleApplication))
                                   .WithReferences(References)
                                   .AddSyntaxTrees(CSharpSyntaxTree
                                   .ParseText(code, new CSharpParseOptions(LanguageVersion.Preview)));

            // Get the root syntax node:
            var root = compilation.SyntaxTrees.First().GetRoot();

            // An error will prent a succesfully compilation:
            var diagnostics = compilation.GetDiagnostics().ToList();

            bool error = diagnostics.Any(diag => diag.Severity == DiagnosticSeverity.Error);

            if (error)
            {
                return new CompilationResult
                {
                    Success = false,
                    Assembly = null,
                    Root = root,
                    Diagnostics = diagnostics.ToList(),
                };
            }

            using (var outputAssembly = new MemoryStream())
            {
                // Output compilation result to the console:
                compilation.Emit(outputAssembly);

                return new CompilationResult
                {
                    Success = true,
                    Assembly = Assembly.Load(outputAssembly.ToArray()),
                    Root = root,
                    Diagnostics = diagnostics.ToList(),
                };
            }
        }

        public void AddNetCoreDefaultReferences()
        {
            var rtPath = Path.GetDirectoryName(typeof(object).Assembly.Location) +
                         Path.DirectorySeparatorChar;

            AddAssemblies(
                rtPath + "System.Private.CoreLib.dll",
                rtPath + "System.Runtime.dll",
                rtPath + "System.Console.dll",
                rtPath + "netstandard.dll",

                rtPath + "System.Text.RegularExpressions.dll", // IMPORTANT!
                rtPath + "System.Linq.dll",
                rtPath + "System.Linq.Expressions.dll", // IMPORTANT!

                rtPath + "System.IO.dll",
                rtPath + "System.Net.Primitives.dll",
                rtPath + "System.Net.Http.dll",
                rtPath + "System.Private.Uri.dll",
                rtPath + "System.Reflection.dll",
                rtPath + "System.ComponentModel.Primitives.dll",
                rtPath + "System.Globalization.dll",
                rtPath + "System.Collections.Concurrent.dll",
                rtPath + "System.Collections.NonGeneric.dll",
                rtPath + "Microsoft.CSharp.dll"
            );
        }

        public void AddAssemblies(params string[] assemblies)
        {
            foreach (var file in assemblies)
                AddAssembly(file);
        }

        public bool AddAssembly(string assemblyDll)
        {
            if (string.IsNullOrEmpty(assemblyDll)) return false;

            var file = Path.GetFullPath(assemblyDll);

            if (!File.Exists(file))
            {
                // check framework or dedicated runtime app folder
                var path = Path.GetDirectoryName(typeof(object).Assembly.Location);
                file = Path.Combine(path, assemblyDll);
                if (!File.Exists(file))
                    return false;
            }

            if (References.Any(r => r.FilePath == file)) return true;

            try
            {
                var reference = MetadataReference.CreateFromFile(file);
                References.Add(reference);
            }
            catch
            {
                return false;
            }

            return true;
        }

    }
}
