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

    public override void SetTypeInfoFromTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name.SetTypeInfoFromTypeRef(typeRef);
        Type?.SetTypeInfoFromTypeRef(typeRef);
    }
}