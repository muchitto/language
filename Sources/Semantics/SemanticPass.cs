using Syntax.Nodes;
using TypeInformation;

namespace Semantics;

public abstract class SemanticPass
{
    protected SemanticContext SemanticContext;

    public Scope CurrentScope => SemanticContext.CurrentScope;

    public abstract void Run(ProgramContainerNode ast, SemanticContext semanticContext);
}