using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Literal;

public class TupleLiteralFieldNode(PositionData positionData, string? name, BaseNode value)
    : LiteralNode(positionData), INodeAcceptor<ILiteralNodeHandler>
{
    public BaseNode Value { get; set; } = value;
    public string? Name { get; set; } = name;

    public void Accept(ILiteralNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not TupleLiteralFieldNode node)
        {
            return false;
        }

        return node.Name == Name && node.Value.TestEquals(Value);
    }
}