namespace TypeInformation;

public enum SymbolResultType
{
    Declared,
    AlreadyDeclared,
    NotDeclared,
    Found,
    NotFound
}

public record struct SymbolResult(TypeRef TypeRef, SymbolResultType ResultType)
{
    public SymbolResult(SymbolResultType resultType) : this(null, resultType)
    {
        ResultType = resultType;
    }
}