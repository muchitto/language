using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Literal;

public class BooleanLiteralNode(PositionData positionData, bool value) : LiteralNode(positionData)
{
    public bool Value { get; set; } = value;

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
        if (other is not BooleanLiteralNode node)
        {
            return false;
        }

        return node.Value == Value;
    }
}