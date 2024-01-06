using Parsing.NodeHandlers;

namespace Parsing.Nodes.Type.Function;

public class FunctionTypeArgumentNode(IdentifierNode? name, TypeNode typeName)
    : TypeNode(name?.PosData ?? typeName.PosData)
{
    public IdentifierNode? Name { get; set; } = name;
    public TypeNode TypeName { get; set; } = typeName;

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }

        Name?.TypeRefAdded();
        TypeName.TypeRefAdded();
    }

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}