using Syntax.Nodes;
using TypeInformation;

namespace Semantics;

public abstract class SemanticPass
{
    protected SemanticContext SemanticContext;

    public Scope CurrentScope => SemanticContext.CurrentScope;

    public void AddNodeToScope(BaseNode node)
    {
        SemanticContext.NodeToScope.Add(node, CurrentScope);
    }

    public abstract void Run(ProgramContainerNode ast, SemanticContext semanticContext);
}