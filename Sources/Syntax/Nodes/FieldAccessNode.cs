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

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Left.SetTypeRef(typeRef);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not FieldAccessNode node)
        {
            return false;
        }

        return Left.TestEquals(node.Left) && Right.TestEquals(node.Right);
    }
}