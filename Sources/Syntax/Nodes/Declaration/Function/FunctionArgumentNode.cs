using Syntax.NodeHandlers;

namespace Syntax.Nodes.Declaration.Function;

public class FunctionArgumentNode(
    IdentifierNode name,
    TypeNode? typeName,
    BaseNode? defaultValue,
    bool isDynamic)
    : BaseNode(name.PosData)
{
    public IdentifierNode Name { get; set; } = name;
    public TypeNode? TypeName { get; set; } = typeName;

    public BaseNode? DefaultValue { get; set; } = defaultValue;

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
        TypeName?.TypeRefAdded();
        DefaultValue?.TypeRefAdded();
    }
}