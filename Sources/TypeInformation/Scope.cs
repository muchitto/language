namespace TypeInformation;

public class Scope(Scope? parent) : ISymbolLookup
{
    public Scope? Parent { get; init; } = parent;
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

    public SymbolLookupResult Add(string name, TypeInfo? typeInfo = null)
    {
        SymbolLookupResult result;

        if (Symbols.TryGetValue(name, out var value))
        {
            result = value.TypeInfo is not UnknownTypeInfo
                ? new SymbolLookupResult(null, SymbolLookupResultType.AlreadyExists)
                : new SymbolLookupResult(value, SymbolLookupResultType.IsUnknown);
        }
        else
        {
            var typeRef = new TypeRef(typeInfo ?? new UnknownTypeInfo(), this);

            Symbols.Add(name, typeRef);

            result = new SymbolLookupResult(typeRef, SymbolLookupResultType.New);
        }

        return result;
    }

    public SymbolLookupResult LookUpOrAdd(string name, TypeInfo? typeInfo = null)
    {
        var result = LookUp(name);

        if (result.ResultType == SymbolLookupResultType.CouldNotFind)
        {
            result = Add(name, typeInfo);
        }

        return result;
    }

    public SymbolLookupResult LookupOrAddOrReplace(string name, TypeInfo? typeInfo = null)
    {
        var result = LookUp(name);

        if (result.ResultType == SymbolLookupResultType.CouldNotFind)
        {
            result = Add(name, typeInfo);
        }
        else if (result.ResultType == SymbolLookupResultType.Found)
        {
            result = result with { ResultType = SymbolLookupResultType.Replaced };

            if (result.TypeRef != null)
            {
                result.TypeRef.TypeInfo = typeInfo ?? new UnknownTypeInfo();
            }
            else
            {
                result = new SymbolLookupResult(null, SymbolLookupResultType.TypeRefIsNull);
            }
        }

        return result;
    }

    public SymbolLookupResult TopScopeLookUpOrAdd(string name, TypeInfo? typeInfo = null)
    {
        var result = TopScope.LookUp(name);

        if (result.ResultType == SymbolLookupResultType.CouldNotFind)
        {
            result = TopScope.Add(name, typeInfo);
        }

        return result;
    }

    public SymbolLookupResult LookUp(string name)
    {
        if (Symbols.TryGetValue(name, out var typeRef))
        {
            return new SymbolLookupResult(typeRef, SymbolLookupResultType.Found);
        }

        return Parent?.LookUp(name) ?? new SymbolLookupResult(null, SymbolLookupResultType.CouldNotFind);
    }
}