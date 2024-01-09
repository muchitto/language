using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes;

public class ArrayAccessNode(BaseNode array, BaseNode access) : BaseNode(array.PositionData)
{
    public BaseNode Array { get; set; } = array;
    public BaseNode AccessExpression { get; set; } = access;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Array.SetTypeRef(typeRef);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not ArrayAccessNode node)
        {
            return false;
        }

        return Array.TestEquals(node.Array) && AccessExpression.TestEquals(node.AccessExpression);
    }
}