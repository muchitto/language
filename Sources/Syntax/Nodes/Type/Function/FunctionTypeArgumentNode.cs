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

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;

        if (Name != null)
        {
            Name.SetTypeRef(typeRef);
        }
        else
        {
            TypeName.SetTypeRef(typeRef);
        }
    }
}