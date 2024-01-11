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

    public override void PropagateTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name.PropagateTypeRef(typeRef);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not TypeAliasDeclarationNode node)
        {
            return false;
        }

        return node.Type.TestEquals(Type) && node.Name.TestEquals(Name);
    }
}