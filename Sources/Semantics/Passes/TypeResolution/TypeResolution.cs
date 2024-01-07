using Syntax.Nodes;

namespace Semantics.Passes.TypeResolution;

public class TypeResolution : SemanticPass
{
    public override void Run(ProgramContainerNode ast, SemanticContext semanticContext)
    {
        SemanticContext = semanticContext;
    }
}