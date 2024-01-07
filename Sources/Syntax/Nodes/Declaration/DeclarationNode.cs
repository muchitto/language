namespace Syntax.Nodes.Declaration;

public abstract class DeclarationNode(IdentifierNode name) : BaseNode(name.PositionData)
{
    public IdentifierNode Name { get; } = name;
}