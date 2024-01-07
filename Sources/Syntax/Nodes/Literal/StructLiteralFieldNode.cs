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

    public override void SetTypeInfoFromTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name.SetTypeInfoFromTypeRef(typeRef);
    }
}