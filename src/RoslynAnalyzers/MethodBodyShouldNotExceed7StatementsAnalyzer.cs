using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;

namespace ConsoleApp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    class MethodBodyShouldNotExceed7StatementsAnalyzer : DiagnosticAnalyzer
    {
        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction<SyntaxKind>(AnalyzeNode, SyntaxKind.MethodDeclaration);
        }
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                return ImmutableArray.Create<DiagnosticDescriptor>(
                     new DiagnosticDescriptor(
            id: "1",
            title: "Method should not contain more than 7 lines",
            messageFormat: "Method '{0}' contains more than 7 lines",
            category: "Design",
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true));

            }
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            //TODO
        }
    }
}
