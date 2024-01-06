namespace Parsing.Nodes;

public abstract class DeclarationNode(IdentifierNode name) : BaseNode(name.PosData)
{
    public IdentifierNode Name { get; } = name;
}