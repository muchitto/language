using Syntax.NodeHandlers;

namespace Syntax.Nodes;

public class ArrayAccessNode(BaseNode array, BaseNode access)
    : BaseNode(array.PositionData), INodeAcceptor<IBasicNodeHandler>
{
    public BaseNode Array { get; set; } = array;
    public BaseNode AccessExpression { get; set; } = access;

    public void Accept(IBasicNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not ArrayAccessNode node)
        {
            return false;
        }

        return Array.TestEquals(node.Array) && AccessExpression.TestEquals(node.AccessExpression);
    }
}