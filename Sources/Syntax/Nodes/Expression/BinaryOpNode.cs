using ErrorReporting;
using Lexing;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Expression;

public class BinaryOpNode(PositionData positionData, BaseNode lhs, Operator @operator, BaseNode rhs)
    : ExpressionNode(positionData), INodeAcceptor<IExpressionNodeHandler>
{
    public BaseNode Lhs { get; set; } = lhs;
    public Operator Operator { get; set; } = @operator;
    public BaseNode Rhs { get; set; } = rhs;

    public void Accept(IExpressionNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not BinaryOpNode node)
        {
            return false;
        }

        return Lhs.TestEquals(node.Lhs)
               && Rhs.TestEquals(node.Rhs)
               && Operator == node.Operator;
    }
}