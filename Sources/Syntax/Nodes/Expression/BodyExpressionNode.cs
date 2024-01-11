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


    public override void PropagateTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not BodyExpressionNode node)
        {
            return false;
        }

        return Statements.TestEquals(node.Statements);
    }
}