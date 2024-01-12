using Syntax.NodeHandlers;

namespace Syntax.Nodes;

public class FieldAccessNode(BaseNode left, BaseNode right)
    : BaseNode(left.PositionData), INodeAcceptor<IBasicNodeHandler>
{
    public BaseNode Left { get; set; } = left;
    public BaseNode Right { get; set; } = right;

    public void Accept(IBasicNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not FieldAccessNode node)
        {
            return false;
        }

        return Left.TestEquals(node.Left) && Right.TestEquals(node.Right);
    }
}