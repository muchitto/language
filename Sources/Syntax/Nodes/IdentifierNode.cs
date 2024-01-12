using ErrorReporting;
using Syntax.NodeHandlers;
using Syntax.Nodes.Type;

namespace Syntax.Nodes;

public class IdentifierNode(PositionData positionData, string name)
    : BaseNode(positionData), INodeAcceptor<IBasicNodeHandler>
{
    public string Name { get; set; } = name;

    public void Accept(IBasicNodeHandler handler)
    {
        handler.Handle(this);
    }

    public static explicit operator IdentifierNode(IdentifierTypeNode node)
    {
        return new IdentifierNode(node.PositionData, node.Name);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not IdentifierNode node)
        {
            return false;
        }

        return node.Name == Name;
    }
}