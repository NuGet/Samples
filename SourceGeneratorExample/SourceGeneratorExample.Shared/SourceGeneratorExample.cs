using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

// File-scoped namespace
namespace SourceGeneratorExample;

[Generator]
#if ROSLYN_4
public class InterfaceStubGeneratorV2 : IIncrementalGenerator
#else
public class InterfaceStubGenerator : ISourceGenerator
#endif
{

#if !ROSLYN_4

    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not SyntaxReceiver receiver)
            return;

        context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.ThisPropertysValueCanBeUsedInTheGenerator", out var someBuildTimeProperty);

        GeneratorCodeWithContext(
            context,
            static (context, diagnostic) => context.ReportDiagnostic(diagnostic),
            static (context, hintName, sourceText) => context.AddSource(hintName, sourceText),
            (CSharpCompilation)context.Compilation,
            someBuildTimeProperty,
            receiver.CandidateProperties.ToImmutableArray());
    }

#endif

    public void GeneratorCodeWithContext<TContext>(
        TContext context,
        Action<TContext, Diagnostic> reportDiagnostic,
        Action<TContext, string, SourceText> addSource,
        CSharpCompilation compilation,
        string? someBuildTimeProperty,
        ImmutableArray<PropertyDeclarationSyntax> candidateProperties)
    {
        // Do something to generate code and call addSource with the contents  

    }

  

#if ROSLYN_4

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // See docs ____ for more details on incremental generators

        // Here we're looking for Properties with an attribute that are in a class
        var candidatePropertiesProvider = context.SyntaxProvider.CreateSyntaxProvider(
            (syntax, cancellationToken) => syntax is PropertyDeclarationSyntax { Parent: ClassDeclarationSyntax, AttributeLists: { Count: > 0 } },
            (context, cancellationToken) => (PropertyDeclarationSyntax)context.Node);

        var someBuildTimeProperty = context.AnalyzerConfigOptionsProvider.Select(
      (analyzerConfigOptionsProvider, cancellationToken) => analyzerConfigOptionsProvider.GlobalOptions.TryGetValue("build_property.ThisPropertysValueCanBeUsedInTheGenerator", out var someBuildTimeProperty) ? someBuildTimeProperty : null);

        var inputs = candidatePropertiesProvider.Collect()
            .Combine(someBuildTimeProperty)
            .Select((combined, cancellationToken) => (candidateProperties: combined.Left, someBuildTimeProperty: combined.Right))    
            .Combine(context.CompilationProvider)
            .Select((combined, cancellationToken) => (combined.Left.candidateProperties, combined.Left.someBuildTimeProperty, compilation: combined.Right));



        context.RegisterSourceOutput(
    inputs,
    (context, collectedValues) =>
    {
        GeneratorCodeWithContext(
            context,
            static (context, diagnostic) => context.ReportDiagnostic(diagnostic),
            static (context, hintName, sourceText) => context.AddSource(hintName, sourceText),
            (CSharpCompilation)collectedValues.compilation,
            collectedValues.someBuildTimeProperty,
            collectedValues.candidateProperties);
    });
    }

#else

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    class SyntaxReceiver : ISyntaxReceiver
    {
        // Example of something to store for later
        public List<PropertyDeclarationSyntax> CandidateProperties { get; } = new();

        /// <summary>
        /// Called for every syntax node in the compilation, we can inspect the nodes and save any information useful for generation
        /// </summary>
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            // Look for things you need to use later in the generator, and store them

            // We're looking for properties with an attribute that are in a class 
            if (syntaxNode is PropertyDeclarationSyntax propertyDeclarationSyntax &&
               propertyDeclarationSyntax.Parent is ClassDeclarationSyntax &&
               propertyDeclarationSyntax.AttributeLists.Count > 0)
            {
                CandidateProperties.Add(propertyDeclarationSyntax);
            }
        }
    }

#endif
}
