using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Literal;

public class TupleLiteralNode(PositionData positionData, List<BaseNode> values)
    : LiteralNode(positionData), INodeAcceptor<ILiteralNodeHandler>
{
    public List<BaseNode> Values { get; set; } = values;

    public void Accept(ILiteralNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not TupleLiteralNode node)
        {
            return false;
        }

        return node.Values.Count == Values.Count && Values.TestEquals(node.Values);
    }
}