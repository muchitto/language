using Lexing;
using Syntax.NodeHandlers;
using Syntax.Nodes.Expression;

namespace Syntax.Nodes.Statement;

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