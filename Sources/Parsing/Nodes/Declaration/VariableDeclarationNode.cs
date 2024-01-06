using Parsing.NodeHandlers;

namespace Parsing.Nodes.Declaration;

public class VariableDeclarationNode(
    IdentifierNode name,
    BaseNode? value,
    bool isLet,
    IdentifierNode? typeName,
    bool isDynamic
)
    : DeclarationNode(name)
{
    public BaseNode? Value { get; set; } = value;
    public bool IsLet { get; set; } = isLet;
    public IdentifierNode? TypeName { get; set; } = typeName;
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
        TypeName?.TypeRefAdded();
    }
}