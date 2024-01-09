using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Expression;

public class FunctionCallNode(BaseNode callee, List<FunctionCallArgumentNode> arguments)
    : ExpressionNode(callee.PositionData)
{
    public BaseNode Callee { get; set; } = callee;
    public List<FunctionCallArgumentNode> Arguments { get; set; } = arguments;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Callee.SetTypeRef(typeRef);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not FunctionCallNode node)
        {
            return false;
        }

        return Callee.TestEquals(node.Callee)
               && Arguments.SequenceEqual(node.Arguments);
    }
}