using Lexing;
using Parsing.NodeHandlers;
using Parsing.Nodes;

namespace Parsing.Node.Expression;

public class BinaryOpNode(PosData posData, BaseNode lhs, BaseNode rhs, Operator @operator)
    : ExpressionNode(posData)
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