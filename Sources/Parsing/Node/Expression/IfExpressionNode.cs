using Lexing;
using Parsing.NodeHandlers;
using Parsing.Nodes;

namespace Parsing.Node.Expression;

public class IfExpressionNode(
    PosData posData,
    BinaryOpNode? condition,
    ExpressionNode body,
    IfExpressionNode nextIf)
    : ExpressionNode(posData)
{
    public BinaryOpNode? Condition { get; set; } = condition;

    public ExpressionNode Body { get; set; } = body;

    public IfExpressionNode? NextIf { get; set; } = nextIf;

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

        Condition?.TypeRefAdded();
        Body.TypeRefAdded();
        NextIf?.TypeRefAdded();
    }
}