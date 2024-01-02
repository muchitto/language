using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Nodes;

public abstract class ExpressionNode : BaseNode
{
    protected ExpressionNode(PosData pos) : base(pos)
    {
    }
}

public class FunctionCallNode(BaseNode callee, List<FunctionCallArgumentNode> arguments)
    : ExpressionNode(callee.PosData)
{
    public BaseNode Callee { get; set; } = callee;
    public List<FunctionCallArgumentNode> Arguments { get; set; } = arguments;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        Callee.Accept(handler);

        foreach (var argument in Arguments)
        {
            argument.Accept(handler);
        }
    }
}

public class FunctionCallArgumentNode(PosData posData, IdentifierNode? name, BaseNode value) : BaseNode(posData)
{
    public IdentifierNode? Name { get; set; } = name;
    public BaseNode Value { get; set; } = value;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        Value.Accept(handler);
        Name?.Accept(handler);
    }
}

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

        Condition?.Accept(handler);
        Body.Accept(handler);
        NextIf?.Accept(handler);
    }
}

public class BinaryOpNode(PosData posData, BaseNode lhs, BaseNode rhs, Operator @operator)
    : ExpressionNode(posData)
{
    public BaseNode Lhs { get; set; } = lhs;
    public BaseNode Rhs { get; set; } = rhs;
    public Operator Operator { get; set; } = @operator;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        Lhs.Accept(handler);
        Rhs.Accept(handler);
    }
}

public class BodyExpressionNode(PosData posData, List<BaseNode> statements) : ExpressionNode(posData)
{
    public List<BaseNode> Statements { get; set; } = statements;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        foreach (var statement in Statements)
        {
            statement.Accept(handler);
        }
    }
}