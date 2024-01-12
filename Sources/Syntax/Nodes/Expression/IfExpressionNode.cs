using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Expression;

public class IfExpressionNode(
    PositionData positionData,
    BinaryOpNode? condition,
    ExpressionNode body,
    IfExpressionNode nextIf)
    : ExpressionNode(positionData), INodeAcceptor<IExpressionNodeHandler>
{
    public BinaryOpNode? Condition { get; set; } = condition;

    public ExpressionNode Body { get; set; } = body;

    public IfExpressionNode? NextIf { get; set; } = nextIf;

    public void Accept(IExpressionNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not IfExpressionNode node)
        {
            return false;
        }

        return Condition.TestEqualsOrBothNull(node.Condition)
               && NextIf.TestEqualsOrBothNull(node.NextIf)
               && Body.TestEquals(node.Body);
    }
}