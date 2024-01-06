using Syntax.Nodes;

namespace Semantics;

public abstract class SemanticPass
{
    protected SemanticContext SemanticContext;

    public abstract void Run(ProgramContainerNode ast, SemanticContext semanticContext);
}