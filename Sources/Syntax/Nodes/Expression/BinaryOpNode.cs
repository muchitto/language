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


    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Lhs.SetTypeRef(typeRef);
    }
}