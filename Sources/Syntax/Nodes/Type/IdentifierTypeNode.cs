using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Type;

public class IdentifierTypeNode(PositionData positionData, string name)
    : TypeNode(positionData), INodeAcceptor<ITypeNodeHandler>
{
    public string Name { get; set; } = name;

    public void Accept(ITypeNodeHandler handler)
    {
        handler.Handle(this);
    }

    public static explicit operator IdentifierTypeNode(IdentifierNode node)
    {
        return new IdentifierTypeNode(node.PositionData, node.Name);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not IdentifierTypeNode node)
        {
            return false;
        }

        return node.Name == Name;
    }
}