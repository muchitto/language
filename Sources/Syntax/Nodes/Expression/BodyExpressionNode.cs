using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Expression;

public class BodyExpressionNode(PositionData positionData, List<BaseNode> statements)
    : ExpressionNode(positionData), INodeAcceptor<IExpressionNodeHandler>
{
    public List<BaseNode> Statements { get; set; } = statements;

    public void Accept(IExpressionNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not BodyExpressionNode node)
        {
            return false;
        }

        return Statements.TestEquals(node.Statements);
    }
}