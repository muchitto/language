using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Expression;

public class IfExpressionNode(
    PositionData positionData,
    BinaryOpNode? condition,
    ExpressionNode body,
    IfExpressionNode nextIf)
    : ExpressionNode(positionData)
{
    public BinaryOpNode? Condition { get; set; } = condition;

    public ExpressionNode Body { get; set; } = body;

    public IfExpressionNode? NextIf { get; set; } = nextIf;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }


    public override void PropagateTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
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