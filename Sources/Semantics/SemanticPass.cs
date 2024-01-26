using Semantics.SymbolResolving;
using Syntax.Nodes;
using Syntax.Nodes.Declaration;

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

    protected void StartDeclarationScope(DeclarationNameNode node)
    {
        SemanticContext.StartScope();

        if (SemanticContext.NodeToScope.TryGetValue(node, out var value))
        {
            SetCurrentScope(value);
        }
        else
        {
            AddNodeToScope(node);
        }
    }

    protected void StartCodeBlockScope(CodeBlockNode node)
    {
        SemanticContext.StartScope();

        AddNodeToScope(node);
    }

    protected void EndScope()
    {
        SemanticContext.EndScope();
    }

    public abstract void Run(ProgramContainerNode ast);
}