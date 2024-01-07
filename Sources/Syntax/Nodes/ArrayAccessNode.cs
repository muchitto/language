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

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }

        Array.TypeRefAdded();
        AccessExpression.TypeRefAdded();
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Array.SetTypeRef(typeRef);
    }
}