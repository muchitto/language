using Syntax.NodeHandlers;
using Syntax.Nodes.Type;
using TypeInformation;

namespace Syntax.Nodes.Declaration.Struct;

public class StructDeclarationNode(
    IdentifierNode name,
    List<StructFieldNode> fields,
    IdentifierNode? parent,
    List<IdentifierTypeNode> interfaces,
    bool implOnly)
    : DeclarationNode(name)
{
    public List<StructFieldNode> Fields { get; set; } = fields;

    public IdentifierNode? Parent { get; set; } = parent;

    public List<IdentifierTypeNode> Interfaces { get; set; } = interfaces;

    public bool ImplOnly { get; set; } = implOnly;

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
        if (other is not StructDeclarationNode node)
        {
            return false;
        }

        if (Interfaces.Any(interfaceNode => !node.Interfaces.Any(x => x.TestEquals(interfaceNode))))
        {
            return false;
        }

        if (Fields.Any(field => !node.Fields.Any(x => x.TestEquals(field))))
        {
            return false;
        }

        return node.ImplOnly == ImplOnly
               && Parent.TestEqualsOrBothNull(node.Parent)
               && node.Name.TestEquals(Name);
    }
}