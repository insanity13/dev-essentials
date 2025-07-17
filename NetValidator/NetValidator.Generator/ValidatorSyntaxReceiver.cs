using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace NetValidator.Generator
{
    class ValidatorSyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> ValidatorCandidates { get; } =  new List<ClassDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classDeclaration && classDeclaration.BaseList != null)
            {
                ValidatorCandidates.Add(classDeclaration);
            }
        }
    }
}
