using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Literal;

public class NumberLiteralNode(PositionData positionData, string value) : LiteralNode(positionData)
{
    public string Value { get; set; } = value;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not NumberLiteralNode node)
        {
            return false;
        }

        return node.Value == Value;
    }
}