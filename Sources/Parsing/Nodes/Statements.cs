using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Nodes;

public abstract class StatementNode : BaseNode
{
    protected StatementNode(PosData posData) : base(posData)
    {
    }
}

public class AssignmentNode(IdentifierNode name, BaseNode value) : StatementNode(name.PosData)
{
    public IdentifierNode Name { get; set; } = name;
    public BaseNode Value { get; set; } = value;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}

public class IfStatementNode(
    PosData posData,
    BinaryOpNode? condition,
    BodyContainerNode bodyContainerNode,
    IfStatementNode? nextIf = null)
    : StatementNode(posData)
{
    public BinaryOpNode? Condition { get; set; } = condition;
    public BodyContainerNode BodyContainerNode { get; set; } = bodyContainerNode;
    public IfStatementNode? NextIf { get; set; } = nextIf;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}

public class ReturnNode(PosData posData, BaseNode? value) : StatementNode(posData)
{
    public BaseNode? Value { get; set; } = value;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}