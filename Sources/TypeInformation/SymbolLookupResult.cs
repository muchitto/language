namespace TypeInformation;

public enum SymbolLookupResultType
{
    Found,
    CouldNotFind,
    New,
    Replaced,
    AlreadyExists,
    TypeRefIsNull,
    IsUnknown
}

public record struct SymbolLookupResult(TypeRef? TypeRef, SymbolLookupResultType ResultType)
{
}