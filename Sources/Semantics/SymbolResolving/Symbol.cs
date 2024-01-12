using TypeInformation;

namespace Semantics.SymbolResolving;

public class Symbol(string name, TypeInfo typeInfo)
{
    public string Name { get; } = name;
    public TypeInfo TypeInfo { get; } = typeInfo;
}