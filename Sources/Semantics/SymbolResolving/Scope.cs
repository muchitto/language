namespace Semantics.SymbolResolving;

public class Scope(List<Symbol> symbols, Scope? parent = null)
{
    public List<Symbol> Symbols { get; set; } = symbols;
    public Scope? Parent { get; set; } = parent;

    public Symbol? LookupSymbol(string name)
    {
        var result = Symbols.FirstOrDefault(symbol => symbol.Name == name);

        return result ?? Parent?.LookupSymbol(name);
    }
}