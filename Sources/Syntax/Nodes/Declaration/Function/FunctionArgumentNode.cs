using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations.Function;

namespace Syntax.Nodes.Declaration.Function;

public class FunctionArgumentNode(
    DeclarationNameNode name,
    TypeNode? typeName,
    BaseNode? defaultValue,
    bool isDynamic)
    : BaseNode(name.PositionData), INodeAcceptor<IFunctionChildNodeHandler>
{
    public DeclarationNameNode Name { get; set; } = name;
    public TypeNode? TypeName { get; set; } = typeName;

    public BaseNode? DefaultValue { get; set; } = defaultValue;

    public bool IsDynamic { get; set; } = isDynamic;

    public void Accept(IFunctionChildNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not FunctionArgumentNode node)
        {
            return false;
        }

        return node.IsDynamic == IsDynamic
               && DefaultValue.TestEqualsOrBothNull(node.DefaultValue)
               && TypeName.TestEqualsOrBothNull(node.TypeName)
               && node.Name.TestEquals(Name);
    }
}