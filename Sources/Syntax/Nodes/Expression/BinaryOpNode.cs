using ErrorReporting;
using Lexing;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Expression;

public class BinaryOpNode(PositionData positionData, BaseNode lhs, BaseNode rhs, Operator @operator)
    : ExpressionNode(positionData)
{
    public BaseNode Lhs { get; set; } = lhs;
    public BaseNode Rhs { get; set; } = rhs;
    public Operator Operator { get; set; } = @operator;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }


    public override void PropagateTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Lhs.PropagateTypeRef(typeRef);
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