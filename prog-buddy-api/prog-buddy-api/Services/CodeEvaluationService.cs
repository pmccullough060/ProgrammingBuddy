using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using prog_buddy_api.Models.Compilation;
using prog_buddy_api.Models.Response;
using System.Linq;

namespace prog_buddy_api.Services
{
    public class CodeEvaluationService
    {
        public CompilationResponseModel GetDiagnostics(CompilationResult result) =>
            new()
            {
                DiagnosticResponseModels = result.Diagnostics.Select(diagnostic => new DiagnosticResponseModel 
                { 
                    Message = diagnostic.Id,
                    Position = GetPosition(result.Root, diagnostic.Location.SourceSpan)
                }).ToList()
            };

        // Get the line position for a given SyntaxNode and TextSpan:
        public Position GetPosition(SyntaxNode node, TextSpan error)
        {
            var span = node.SyntaxTree.GetLineSpan(error);

            return new Position 
            { 
                Line = span.StartLinePosition.Line + 1, // Line position is zero index:
                Start = span.StartLinePosition.Character,
                End = span.EndLinePosition.Character,
            };
        }

        public void SyntaxEvaluation(SyntaxNode root)
        {
            var variableDeclarations = root.DescendantNodes().OfType<VariableDeclarationSyntax>();

            var variableAssignments = root.DescendantNodes().OfType<AssignmentExpressionSyntax>();
        }
    }
}
