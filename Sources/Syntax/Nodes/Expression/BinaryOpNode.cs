using ErrorReporting;
using Lexing;
using Syntax.NodeHandlers;

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

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }

        Lhs.TypeRefAdded();
        Rhs.TypeRefAdded();
    }
}