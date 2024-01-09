using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Literal;

public class StructLiteralFieldNode(PositionData positionData, IdentifierNode name, BaseNode value)
    : LiteralNode(positionData)
{
    public IdentifierNode Name { get; set; } = name;
    public BaseNode Value { get; set; } = value;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name.SetTypeRef(typeRef);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not StructLiteralFieldNode node)
        {
            return false;
        }

        return node.Name.TestEquals(Name) && node.Value.TestEquals(Value);
    }
}