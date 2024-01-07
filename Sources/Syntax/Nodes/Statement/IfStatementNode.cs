using ErrorReporting;
using Syntax.NodeHandlers;
using Syntax.Nodes.Expression;
using TypeInformation;

namespace Syntax.Nodes.Statement;

public class IfStatementNode(
    PositionData positionData,
    BinaryOpNode? condition,
    BodyContainerNode bodyContainerNode,
    IfStatementNode? nextIf = null)
    : StatementNode(positionData)
{
    public BinaryOpNode? Condition { get; set; } = condition;
    public BodyContainerNode BodyContainerNode { get; set; } = bodyContainerNode;
    public IfStatementNode? NextIf { get; set; } = nextIf;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
    }
}