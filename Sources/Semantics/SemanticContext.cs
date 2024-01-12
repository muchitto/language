using Semantics.SymbolResolving;
using Syntax.Nodes;

namespace Semantics;

public class SemanticContext
{
    public Dictionary<IdentifierNode, Symbol> IdentifierToSymbol = new();
    public Dictionary<BaseNode, Scope> NodeToScope = new();
    public Scope CurrentScope { get; set; }

    public void StartScope()
    {
        var scope = new Scope([])
        {
            Parent = CurrentScope
        };

        CurrentScope = scope;
    }

    public void EndScope()
    {
        if (CurrentScope.Parent != null)
        {
            CurrentScope = CurrentScope.Parent;
        }
    }

    public Symbol? GetSymbol(IdentifierNode identifierNode)
    {
        return IdentifierToSymbol.GetValueOrDefault(identifierNode);
    }

    public Symbol? SetSymbol(IdentifierNode identifierNode, Symbol symbol)
    {
        return IdentifierToSymbol[identifierNode] = symbol;
    }
}