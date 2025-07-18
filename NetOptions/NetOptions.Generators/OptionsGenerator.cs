using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetOptions.Generators
{
    namespace OptionsGenerator
    {
        [Generator]
        public class OptionsGenerator : ISourceGenerator
        {
            public void Initialize(GeneratorInitializationContext context)
            {
                context.RegisterForSyntaxNotifications(() => new OptionsSyntaxReceiver());
            }

            public void Execute(GeneratorExecutionContext context)
            {
                if (!(context.SyntaxContextReceiver is OptionsSyntaxReceiver receiver))
                    return;

                if (receiver.OptionsClasses.Count == 0)
                    return;

                GenerateGlobalOptionsRegistration(context, receiver.OptionsClasses);
            }

            private void GenerateGlobalOptionsRegistration(
                GeneratorExecutionContext context,
                List<INamedTypeSymbol> optionsClasses)
            {
                var namespaceName = "NetOptions.Core";
                var registrations = new StringBuilder();
                var validateOnStartRegistrations = new StringBuilder();

                foreach (var classSymbol in optionsClasses)
                {
                    var attribute = classSymbol.GetAttributes()
                        .First(ad => ad.AttributeClass?.Name == "OptionsAttribute");

                    var section = attribute.ConstructorArguments[0].Value?.ToString();
                    var validateOnStart = attribute.ConstructorArguments.Length <= 1 || (bool)(attribute.ConstructorArguments[1].Value ?? true);

                    var className = classSymbol.Name;
                    var fullTypeName = classSymbol.ToDisplayString();

                    registrations.Append($"services.AddOptions<{fullTypeName}>().BindConfiguration(\"{section}\").ValidateDataAnnotations()");

                    if (validateOnStart)
                        registrations.Append(".ValidateOnStart()");

                    registrations.Append(";");
                    registrations.AppendLine();
                }

                var source = $@"
                using Microsoft.Extensions.DependencyInjection;
                using Microsoft.Extensions.Options;

                namespace {namespaceName}
                {{
                    public static partial class OptionsRegistrationExtensions
                    {{
                        public static IServiceCollection AddAllOptions(this IServiceCollection services)
                        {{
                            {registrations}

                            {validateOnStartRegistrations}

                            return services;
                        }}
                    }}
                }}";

                context.AddSource("OptionsRegistrationExtensions.g.cs", SourceText.From(source, Encoding.UTF8));
            }
        }

        class OptionsSyntaxReceiver : ISyntaxContextReceiver
        {
            public List<INamedTypeSymbol> OptionsClasses { get; } = new List<INamedTypeSymbol>();

            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
            {
                if (context.Node is Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax classDeclaration)
                {
                    var symbol = context.SemanticModel.GetDeclaredSymbol(classDeclaration) as INamedTypeSymbol;
                    if (symbol?.GetAttributes().Any(ad => ad.AttributeClass?.Name == "OptionsAttribute") == true)
                    {
                        OptionsClasses.Add(symbol);
                    }
                }
            }
        }
    }
}
