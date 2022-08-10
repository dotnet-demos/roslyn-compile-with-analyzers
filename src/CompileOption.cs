using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
namespace ConsoleApp
{
    class CompileOption
    {
        CompilationProvider dependency;
        ILogger<CompileOption> logger;
        public CompileOption(CompilationProvider dep, ILogger<CompileOption> logger)
        {
            dependency = dep;
            this.logger = logger;
        }
        async internal Task Execute()
        {
            logger.LogTrace($"{nameof(CompileWithAnalyzersOption)} : Start");

            Compilation compilation = dependency.GetSimpleCompilation();
            ImmutableArray<Diagnostic> diagnostics = await Task.Run(()=> Compile(compilation,Configurations.OutputFileLocation));
            diagnostics.ToImmutableList().ForEach(diagnostic => Console.WriteLine(diagnostic));
            logger.LogInformation($"{nameof(Execute)} - Compilation completed. Output location - {Configurations.OutputFileLocation}");
        }
        private ImmutableArray<Diagnostic> Compile(Compilation compilation,string outputDllFilePathName)
        {
            EmitResult result = compilation.Emit(outputDllFilePathName);
            return result.Diagnostics;
        }
    }
}