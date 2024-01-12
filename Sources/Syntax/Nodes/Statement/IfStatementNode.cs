using ErrorReporting;
using Syntax.NodeHandlers;
using Syntax.Nodes.Expression;

namespace Syntax.Nodes.Statement;

public class IfStatementNode(
    PositionData positionData,
    BinaryOpNode? condition,
    BodyContainerNode bodyContainerNode,
    IfStatementNode? nextIf = null)
    : StatementNode(positionData), INodeAcceptor<IStatementNodeHandler>
{
    public BinaryOpNode? Condition { get; set; } = condition;
    public BodyContainerNode BodyContainerNode { get; set; } = bodyContainerNode;
    public IfStatementNode? NextIf { get; set; } = nextIf;

    public void Accept(IStatementNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not IfStatementNode node)
        {
            return false;
        }

        return Condition.TestEqualsOrBothNull(node.Condition)
               && BodyContainerNode.TestEquals(node.BodyContainerNode)
               && nextIf.TestEqualsOrBothNull(node.NextIf);
    }
}