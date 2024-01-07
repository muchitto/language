using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes;

public class FieldAccessNode(BaseNode left, BaseNode right) : BaseNode(left.PositionData)
{
    public BaseNode Left { get; set; } = left;
    public BaseNode Right { get; set; } = right;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void SetTypeInfoFromTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Left.SetTypeInfoFromTypeRef(typeRef);
    }
}