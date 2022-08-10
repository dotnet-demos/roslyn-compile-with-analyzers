using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleApp
{
    class CompilationProvider
    {
        private readonly ILogger<CompilationProvider> _logger;
        ISourceCodeProvider dependency;
        public CompilationProvider(ISourceCodeProvider dep, ILogger<CompilationProvider> logger)
        {
            _logger = logger;
            dependency = dep;
        }
        public Compilation GetSimpleCompilation()
        {
            string source = dependency.Get();
            SyntaxTree tree = GetSyntaxTree(source);
            IEnumerable<MetadataReference> referecnes = GetReferences();

            return CSharpCompilation.Create("TestRoslynOutput",
                syntaxTrees: new[] { tree },
                references: referecnes,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                );
        }
        private IEnumerable<MetadataReference> GetReferences()
        {
            var references = new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            };
            // Above is not enough. Please refer https://github.com/dotnet/roslyn/issues/49498

            Assembly.GetEntryAssembly().GetReferencedAssemblies()
            .ToList()
            .ForEach(a => references.Add(MetadataReference.CreateFromFile(Assembly.Load(a).Location)));
            return references;
        }
        private SyntaxTree GetSyntaxTree(string source)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);
            return syntaxTree;
        }
    }
}