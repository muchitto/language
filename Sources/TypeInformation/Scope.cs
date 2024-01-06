namespace TypeInformation;

public enum ScopeType
{
    Regular,
    Declaration
}

public class Scope
{
    public Scope(Scope? parent, ScopeType scopeType)
    {
        Parent = parent;
        ScopeType = scopeType;
        Parent?.Children.Add(this);
    }

    public Scope(ScopeType scopeType) : this(null, scopeType)
    {
    }

    public ScopeType ScopeType { get; init; }

    public Scope? Parent { get; init; }

    public List<Scope> Children { get; } = new();

    public Dictionary<string, TypeRef> Symbols { get; } = new();

    public Scope TopScope
    {
        get
        {
            var scope = this;

            while (scope.Parent != null)
            {
                scope = scope.Parent;
            }

            return scope;
        }
    }

    public TypeRef? LookupSymbolFromCurrentScope(string name)
    {
        return Symbols.GetValueOrDefault(name);
    }

    public SymbolLookupResult LookupSymbol(string name)
    {
        var scope = this;

        var crossedDeclarationBoundary = false;
        while (scope != null)
        {
            var symbol = scope.LookupSymbolFromCurrentScope(name);

            if (symbol != null)
            {
                return new SymbolLookupResult(symbol, crossedDeclarationBoundary);
            }

            if (scope.ScopeType == ScopeType.Declaration)
            {
                crossedDeclarationBoundary = true;
            }

            scope = scope.Parent;
        }

        return new SymbolLookupResult(null, crossedDeclarationBoundary);
    }
}