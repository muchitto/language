using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Literal;

public class TupleLiteralNode(PositionData positionData, List<BaseNode> values) : LiteralNode(positionData)
{
    public List<BaseNode> Values { get; set; } = values;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
    }
}