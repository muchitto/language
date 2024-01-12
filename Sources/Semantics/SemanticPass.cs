using Semantics.SymbolResolving;
using Syntax.Nodes;

namespace Semantics;

public abstract class SemanticPass(SemanticContext semanticContext)
{
    protected SemanticContext SemanticContext { get; } = semanticContext;

    protected Scope CurrentScope => SemanticContext.CurrentScope;

    protected void AddNodeToScope(BaseNode node)
    {
        SemanticContext.NodeToScope.Add(node, CurrentScope);
    }

    private void SetCurrentScope(Scope scope)
    {
        SemanticContext.CurrentScope = scope;
    }

    protected void StartScopeFromNode(BaseNode node)
    {
        if (!SemanticContext.NodeToScope.TryGetValue(node, out var value))
        {
            throw new Exception("Node not found in scope");
        }

        SetCurrentScope(value);
    }

    protected void StartScope()
    {
        SemanticContext.StartScope();
    }

    protected void EndScope()
    {
        SemanticContext.EndScope();
    }

    public abstract void Run(ProgramContainerNode ast);
}