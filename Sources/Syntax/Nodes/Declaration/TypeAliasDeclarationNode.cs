using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Declaration;

public class TypeAliasDeclarationNode(IdentifierNode name, TypeNode type) : DeclarationNode(name)
{
    public TypeNode Type { get; set; } = type;

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

        Name.TypeRefAdded();
        Type.TypeRefAdded();
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name.SetTypeRef(typeRef);
    }
}