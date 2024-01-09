using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Literal;

public class CharLiteralNode(PositionData positionData, char value) : LiteralNode(positionData)
{
    public char Value { get; set; } = value;

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
        if (other is not CharLiteralNode node)
        {
            return false;
        }

        return node.Value == Value;
    }
}