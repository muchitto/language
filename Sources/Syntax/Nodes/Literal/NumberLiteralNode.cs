using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Literal;

public class NumberLiteralNode(PositionData positionData, string value)
    : LiteralNode(positionData), INodeAcceptor<ILiteralNodeHandler>
{
    public string Value { get; set; } = value;

    public void Accept(ILiteralNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not NumberLiteralNode node)
        {
            return false;
        }

        return node.Value == Value;
    }
}