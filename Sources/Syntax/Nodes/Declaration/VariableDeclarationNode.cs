using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Declaration;

public class VariableDeclarationNode(
    IdentifierNode name,
    BaseNode? value,
    bool isLet,
    TypeNode? typeNode,
    bool isDynamic
)
    : DeclarationNode(name)
{
    public BaseNode? Value { get; set; } = value;
    public bool IsLet { get; set; } = isLet;
    public TypeNode? Type { get; set; } = typeNode;
    public bool IsDynamic { get; set; } = isDynamic;

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
        Value?.TypeRefAdded();
        Type?.TypeRefAdded();
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name.SetTypeRef(typeRef);
        Type?.SetTypeRef(typeRef);
    }
}