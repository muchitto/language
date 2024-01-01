using TypeInformation;

namespace Semantics;

public class SemanticContext : ISymbolLookup
{
    public List<Scope> AllScopes = [];

    public Scope CurrentScope { get; private set; }

    public Scope TopScope => CurrentScope.TopScope;

    public bool IsCurrentScopeTopScope => CurrentScope == TopScope;

    public SymbolLookupResult LookUp(string name)
    {
        return CurrentScope.LookUp(name);
    }

    public SymbolLookupResult Add(string name, TypeInfo? typeInfo = null)
    {
        return CurrentScope.Add(name, typeInfo);
    }

    public SymbolLookupResult LookUpOrAdd(string name, TypeInfo? typeInfo = null)
    {
        return CurrentScope.LookUpOrAdd(name, typeInfo);
    }

    public SymbolLookupResult LookupOrAddOrReplace(string name, TypeInfo? typeInfo = null)
    {
        return CurrentScope.LookupOrAddOrReplace(name, typeInfo);
    }

    public SymbolLookupResult TopScopeLookUpOrAdd(string name, TypeInfo? typeInfo = null)
    {
        return TopScope.LookUpOrAdd(name, typeInfo);
    }

    public List<TypeRef> TypeRefsFromAllScopes()
    {
        return AllScopes.SelectMany(scope => scope.Symbols.ToList().Select(x => x.Value)).ToList();
    }

    public Scope NewScope()
    {
        var newScope = new Scope(CurrentScope);

        AllScopes.Add(newScope);

        CurrentScope = newScope;

        return newScope;
    }

    public void PopScope()
    {
        if (CurrentScope.Parent != null)
        {
            CurrentScope = CurrentScope.Parent;
        }
    }
}