namespace TypeInformation;

public interface ISymbolLookup
{
    public SymbolLookupResult LookUp(string name);
    public SymbolLookupResult Add(string name, TypeInfo? typeInfo = null);
    public SymbolLookupResult LookUpOrAdd(string name, TypeInfo? typeInfo = null);
    public SymbolLookupResult LookupOrAddOrReplace(string name, TypeInfo? typeInfo = null);
    public SymbolLookupResult TopScopeLookUpOrAdd(string name, TypeInfo? typeInfo = null);
}