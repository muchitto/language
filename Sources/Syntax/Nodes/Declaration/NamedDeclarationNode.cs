namespace Syntax.Nodes.Declaration;

public abstract class NamedDeclarationNode(DeclarationNameNode name) : DeclarationNode(name.PositionData)
{
    public DeclarationNameNode Name { get; } = name;
}