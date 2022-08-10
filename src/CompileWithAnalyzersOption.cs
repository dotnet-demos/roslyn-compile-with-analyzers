using EasyConsole;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
namespace ConsoleApp
{
    class CompileWithAnalyzersOption
    {
        CompilationProvider dependency;
        ILogger<CompileWithAnalyzersOption> logger;
        public CompileWithAnalyzersOption(CompilationProvider dep, ILogger<CompileWithAnalyzersOption> logger)
        {
            dependency = dep;
            this.logger = logger;
        }
        async internal Task Execute()
        {
            logger.LogTrace($"{nameof(CompileWithAnalyzersOption)} : Start");
            await TestRoslynCompilationWithAnalyzers();
        }

        public async Task TestRoslynCompilationWithAnalyzers()
        {
            Compilation compilation = dependency.GetSimpleCompilation();
            CompilationWithAnalyzers compilationWithAnalyzers = GetAnalyzerAwareCompilation(compilation);
            
            ImmutableArray<Diagnostic> diagnosticResults = await compilationWithAnalyzers.GetAllDiagnosticsAsync();
            if (diagnosticResults.Length == 0)
            {
                ImmutableArray<Diagnostic> diagnostics = Compile(compilationWithAnalyzers.Compilation,Configurations.OutputFileLocation);
                diagnostics.ToImmutableList().ForEach(diagnostic => Console.WriteLine(diagnostic));
            }
            else
            {
                diagnosticResults.ToImmutableList().ForEach(diagnostic => Console.WriteLine(diagnostic));
            }
        }
        private CompilationWithAnalyzers GetAnalyzerAwareCompilation(Compilation compilation)
        {
            ImmutableArray<DiagnosticAnalyzer> analyzers = ImmutableArray.Create<DiagnosticAnalyzer>(
                new TypeNameShouldStartWithCapitalLetterDiagnosticAnalyzer(),
                new MethodBodyShouldNotExceed7StatementsAnalyzer());

            CompilationWithAnalyzers compilationWithAnalyzers = new CompilationWithAnalyzers(compilation, analyzers,
                GetAnalyzerOptions(),
                new CancellationToken());
            return compilationWithAnalyzers;
        }

        private AnalyzerOptions GetAnalyzerOptions()
        {
            return new AnalyzerOptions(ImmutableArray<AdditionalText>.Empty);
        }

        private ImmutableArray<Diagnostic> Compile(Compilation compilation, string outputDllFilePathName)
        {
            EmitResult result = compilation.Emit(outputDllFilePathName);
            return result.Diagnostics;
        }
       
    }
}