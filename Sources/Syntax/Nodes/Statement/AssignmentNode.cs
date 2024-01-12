using Syntax.NodeHandlers;

namespace Syntax.Nodes.Statement;

public class AssignmentNode(BaseNode name, BaseNode value)
    : StatementNode(name.PositionData), INodeAcceptor<IStatementNodeHandler>
{
    public BaseNode Name { get; set; } = name;
    public BaseNode Value { get; set; } = value;

    public void Accept(IStatementNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not AssignmentNode node)
        {
            return false;
        }

        return node.Name.TestEquals(Name) && node.Value.TestEquals(Value);
    }
}