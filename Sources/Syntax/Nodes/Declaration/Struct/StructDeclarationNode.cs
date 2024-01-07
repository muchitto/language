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

    public override void SetTypeInfoFromTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name.SetTypeInfoFromTypeRef(typeRef);
    }
}