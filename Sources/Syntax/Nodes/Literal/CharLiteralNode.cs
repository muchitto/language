using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Literal;

public class CharLiteralNode(PositionData positionData, char value)
    : LiteralNode(positionData), INodeAcceptor<ILiteralNodeHandler>
{
    public char Value { get; set; } = value;

    public void Accept(ILiteralNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not CharLiteralNode node)
        {
            return false;
        }

        return node.Value == Value;
    }
}