using Syntax.NodeHandlers;

namespace Syntax.Nodes.Expression;

public class FunctionCallNode(BaseNode callee, List<FunctionCallArgumentNode> arguments)
    : ExpressionNode(callee.PositionData), INodeAcceptor<IExpressionNodeHandler>
{
    public BaseNode Callee { get; set; } = callee;
    public List<FunctionCallArgumentNode> Arguments { get; set; } = arguments;

    public void Accept(IExpressionNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not FunctionCallNode node)
        {
            return false;
        }

        return Callee.TestEquals(node.Callee) && Arguments.TestEquals(node.Arguments);
    }
}