using Semantics.Passes.DeclarationPass;
using Semantics.Passes.TypeResolutionPass;
using Syntax.Nodes;

namespace Semantics;

public class SemanticChecks
{
    public void RunPass(ProgramContainerNode ast)
    {
        var semanticInfo = new SemanticContext();

        List<SemanticPass> passes =
        [
            new DeclarationPass(semanticInfo),
            new TypeResolutionPass(semanticInfo)
        ];

        foreach (var pass in passes)
        {
            pass.Run(ast);
        }
    }
}