using Parsing.NodeHandlers;
using Parsing.Nodes.Type;

namespace Parsing.Node.Declaration.Struct;

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

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }

        Name.TypeRefAdded();
        Parent?.TypeRefAdded();
        foreach (var @interface in Interfaces)
        {
            @interface.TypeRefAdded();
        }
    }
}