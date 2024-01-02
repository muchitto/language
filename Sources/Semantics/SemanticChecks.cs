using Parsing.Nodes;
using Semantics.Passes;

namespace Semantics;

public class SemanticChecks
{
    private readonly List<SemanticPass> _passes =
    [
        new TypeRefAssignment()
    ];

    public void RunPass(ProgramContainerNode ast)
    {
        var semanticInfo = new SemanticContext();
        foreach (var pass in _passes)
        {
            pass.Run(ast, semanticInfo);
        }
    }
}