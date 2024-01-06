namespace Parsing.Node.Declaration;

public abstract class DeclarationNode(IdentifierNode name) : BaseNode(name.PosData)
{
    public IdentifierNode Name { get; } = name;
}