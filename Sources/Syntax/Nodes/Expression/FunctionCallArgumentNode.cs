using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Expression;

public class FunctionCallArgumentNode(PositionData positionData, IdentifierNode? name, BaseNode value)
    : BaseNode(positionData), INodeAcceptor<IExpressionNodeHandler>
{
    public IdentifierNode? Name { get; set; } = name;
    public BaseNode Value { get; set; } = value;

    public void Accept(IExpressionNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not FunctionCallArgumentNode node)
        {
            return false;
        }

        return Name.TestEqualsOrBothNull(node.Name) && node.Value.TestEquals(Value);
    }
}