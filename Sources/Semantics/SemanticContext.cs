using TypeInformation;

namespace Semantics;

public class SemanticContext : ISymbolLookup
{
    public readonly List<Scope> AllScopes = [];

    public Scope CurrentScope { get; private set; }

    public Scope TopScope => CurrentScope.TopScope;

    public bool IsCurrentScopeTopScope => CurrentScope == TopScope;

    public SymbolResult LookupTypeRef(string name)
    {
        return CurrentScope.LookupTypeRef(name);
    }

    public SymbolResult LookupUntilDeclarationBoundary(string name)
    {
        return CurrentScope.LookupUntilDeclarationBoundary(name);
    }

    public SymbolResult CollectDeclaration(string name)
    {
        return CurrentScope.CollectDeclaration(name);
    }

    public SymbolResult CollectDeclaration(string name, TypeRef typeRef)
    {
        return CurrentScope.CollectDeclaration(name, typeRef);
    }

    public SymbolResult CollectVariable(string name)
    {
        return CurrentScope.CollectVariable(name);
    }

    public SymbolResult CollectVariable(string name, TypeRef typeRef)
    {
        return CurrentScope.CollectVariable(name, typeRef);
    }

    public SymbolResult SetupDeclaration(string name, TypeRef typeRef)
    {
        return CurrentScope.SetupDeclaration(name, typeRef);
    }

    public Scope StartScope(ScopeType scopeType)
    {
        var newScope = new Scope(CurrentScope, scopeType);

        AllScopes.Add(newScope);

        CurrentScope = newScope;

        return newScope;
    }

    public void EndScope()
    {
        if (CurrentScope.Parent != null)
        {
            CurrentScope = CurrentScope.Parent;
        }
    }
}