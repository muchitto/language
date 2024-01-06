namespace TypeInformation;

public enum ScopeType
{
    Regular,
    Declaration
}

public class Scope : ISymbolLookup
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

    public SymbolResult LookupTypeRef(string name)
    {
        var scope = this;

        while (scope != null)
        {
            if (scope.Symbols.TryGetValue(name, out var value))
            {
                return new SymbolResult(value, SymbolResultType.Found);
            }

            scope = scope.Parent;
        }

        return new SymbolResult(SymbolResultType.NotFound);
    }

    public SymbolResult LookupUntilDeclarationBoundary(string name)
    {
        var scope = this;

        while (scope != null)
        {
            if (scope.Symbols.TryGetValue(name, out var value))
            {
                return new SymbolResult(value, SymbolResultType.Found);
            }

            if (scope.ScopeType == ScopeType.Declaration)
            {
                return new SymbolResult(SymbolResultType.NotFound);
            }

            scope = scope.Parent;
        }

        return new SymbolResult(SymbolResultType.NotFound);
    }

    public SymbolResult SetupDeclaration(string name, TypeRef typeRef)
    {
        var result = LookupUntilDeclarationBoundary(name);

        if (result.ResultType == SymbolResultType.Found)
        {
            return new SymbolResult(SymbolResultType.AlreadyDeclared);
        }

        Symbols.Add(name, typeRef);

        return new SymbolResult(SymbolResultType.Declared);
    }

    public SymbolResult CollectDeclaration(string name)
    {
        return CollectDeclaration(name, TypeRef.Unknown(this));
    }

    public SymbolResult CollectDeclaration(string name, TypeRef typeRef)
    {
        var result = LookupUntilDeclarationBoundary(name);

        if (result.ResultType == SymbolResultType.Found)
        {
            return new SymbolResult(SymbolResultType.AlreadyDeclared);
        }

        Symbols.Add(name, typeRef);

        return new SymbolResult(SymbolResultType.Declared);
    }

    public SymbolResult CollectVariable(string name)
    {
        return CollectVariable(name, TypeRef.Unknown(this));
    }

    public SymbolResult CollectVariable(string name, TypeRef typeRef)
    {
        var scope = this;

        while (scope != null)
        {
            if (scope.Symbols.TryGetValue(name, out var value))
            {
                return new SymbolResult(value, SymbolResultType.Declared);
            }

            scope = scope.Parent;
        }

        Symbols.Add(name, typeRef);

        return new SymbolResult(SymbolResultType.NotDeclared);
    }
}