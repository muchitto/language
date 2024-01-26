using ErrorReporting;

namespace Syntax.Nodes.Declaration;

public class DeclarationNameNode(PositionData positionData, string name) : IdentifierNode(positionData, name)
{
    // Generate a explicit conversion from IdentifierNode to DeclarationNameNode
    public DeclarationNameNode(IdentifierNode identifier) : this(identifier.PositionData, identifier.Name)
    {
    }
}