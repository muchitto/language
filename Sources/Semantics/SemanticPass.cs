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

    public void SetCurrentScope(Scope scope)
    {
        SemanticContext.SetCurrentScope(scope);
    }

    public void StartScopeFromNode(BaseNode node)
    {
        if (!SemanticContext.NodeToScope.TryGetValue(node, out var value))
        {
            throw new Exception("Node not found in scope");
        }

        SetCurrentScope(value);
    }

    protected void StartScope(ScopeType scopeType)
    {
        SemanticContext.StartScope(scopeType);
    }

    protected void EndScope()
    {
        SemanticContext.EndScope();
    }

    public abstract void Run(ProgramContainerNode ast, SemanticContext semanticContext);
}