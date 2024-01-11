using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Type.Function;

public class FunctionTypeArgumentNode(IdentifierNode? name, TypeNode typeName)
    : TypeNode(name?.PositionData ?? typeName.PositionData)
{
    public IdentifierNode? Name { get; set; } = name;
    public TypeNode TypeName { get; set; } = typeName;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void PropagateTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;

        Name?.PropagateTypeRef(typeRef);
        TypeName.PropagateTypeRef(typeRef);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not FunctionTypeArgumentNode node)
        {
            return false;
        }

        return Name.TestEqualsOrBothNull(node.Name) && TypeName.TestEqualsOrBothNull(node.TypeName);
    }

    public override TypeRef ResultingType()
    {
        return TypeRef;
    }
}