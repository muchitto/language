using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Literal;

public class BooleanLiteralNode(PositionData positionData, bool value)
    : LiteralNode(positionData), INodeAcceptor<ILiteralNodeHandler>
{
    public bool Value { get; set; } = value;

    public void Accept(ILiteralNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not BooleanLiteralNode node)
        {
            return false;
        }

        return node.Value == Value;
    }
}