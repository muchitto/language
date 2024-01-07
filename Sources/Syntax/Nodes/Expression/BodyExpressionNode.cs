using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Expression;

public class BodyExpressionNode(PositionData positionData, List<BaseNode> statements) : ExpressionNode(positionData)
{
    public List<BaseNode> Statements { get; set; } = statements;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }


    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
    }
}