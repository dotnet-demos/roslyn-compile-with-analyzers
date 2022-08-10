using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace ConsoleApp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class TypeNameShouldStartWithCapitalLetterDiagnosticAnalyzer : DiagnosticAnalyzer
    {
        internal const string DiagnosticId = "TypeNameShouldStartWithCapitalLetter";

        internal static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            id: DiagnosticId,
            title: "Type name starts with lowercase letters",
            messageFormat: "Type name '{0}' starts with lowercase letters",
            category: "Naming",
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true);
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }
        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
        }
        private void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            if (char.IsLower(context.Symbol.Name.First()))
            {
                var diagnostic = Diagnostic.Create(Rule, context.Symbol.Locations[0], context.Symbol.Name);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
