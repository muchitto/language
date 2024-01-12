using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Statement;

public class ReturnNode(PositionData positionData, BaseNode? value)
    : StatementNode(positionData), INodeAcceptor<IStatementNodeHandler>
{
    public BaseNode? Value { get; set; } = value;

    public void Accept(IStatementNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        return other is ReturnNode node && Value.TestEqualsOrBothNull(node.Value);
    }
}