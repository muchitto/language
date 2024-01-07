using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Declaration.Enum;

public class EnumCaseAssociatedValueNode(PositionData positionData, IdentifierNode? name, IdentifierNode type)
    : BaseNode(positionData)
{
    public IdentifierNode? Name { get; set; } = name;
    public IdentifierNode Type { get; set; } = type;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name?.SetTypeRef(typeRef);
        Type.SetTypeRef(typeRef);
    }
}