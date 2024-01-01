using Parsing.Nodes;
using Semantics.Passes;

namespace Semantics;

public class SemanticChecks
{
    private readonly List<SemanticPass> _passes =
    [
        new TypeRefAssignment(),
        new TypeResolutionPass(),
        new TypePropagationPass(),
        new TypeCheckPass(),
        new ExceptionChecker(),
        new NullHandleChecker()
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