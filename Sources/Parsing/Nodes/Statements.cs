using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Nodes;

public abstract class StatementNode : BaseNode
{
    protected StatementNode(PosData posData) : base(posData)
    {
    }
}

public class AssignmentNode(BaseNode name, BaseNode value) : StatementNode(name.PosData)
{
    public BaseNode Name { get; set; } = name;
    public BaseNode Value { get; set; } = value;

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

        Name.TypeRefAdded();
        Value.TypeRefAdded();
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

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }

        Condition?.TypeRefAdded();
        BodyContainerNode.TypeRefAdded();
        NextIf?.TypeRefAdded();
    }
}

public class ReturnNode(PosData posData, BaseNode? value) : StatementNode(posData)
{
    public BaseNode? Value { get; set; } = value;

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

        Value?.TypeRefAdded();
    }
}