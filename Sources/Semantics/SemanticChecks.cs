using Semantics.Passes.DeclarationPass;
using Syntax.Nodes;

namespace Semantics;

public class SemanticChecks
{
    public void RunPass(ProgramContainerNode ast)
    {
        var semanticInfo = new SemanticContext();

        List<SemanticPass> passes =
        [
            new DeclarationPass(semanticInfo)
        ];

        foreach (var pass in passes)
        {
            pass.Run(ast);
        }
    }
}