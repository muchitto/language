using Syntax.NodeHandlers;

namespace Syntax.Nodes.Type.Function;

public class FunctionTypeArgumentNode(IdentifierNode? name, TypeNode typeName)
    : TypeNode(name?.PositionData ?? typeName.PositionData), INodeAcceptor<ITypeNodeHandler>
{
    public IdentifierNode? Name { get; set; } = name;
    public TypeNode TypeName { get; set; } = typeName;

    public void Accept(ITypeNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not FunctionTypeArgumentNode node)
        {
            return false;
        }

        return Name.TestEqualsOrBothNull(node.Name) && TypeName.TestEqualsOrBothNull(node.TypeName);
    }
}