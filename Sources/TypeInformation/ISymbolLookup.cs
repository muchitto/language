namespace TypeInformation;

public interface ISymbolLookup
{
    public SymbolResult LookupTypeRef(string name);

    public SymbolResult LookupUntilDeclarationBoundary(string name);

    public SymbolResult CollectDeclaration(string name);

    public SymbolResult CollectDeclaration(string name, TypeRef typeRef);

    public SymbolResult SetupDeclaration(string name, TypeRef typeRef);

    public SymbolResult CollectVariable(string name);

    public SymbolResult CollectVariable(string name, TypeRef typeRef);
}