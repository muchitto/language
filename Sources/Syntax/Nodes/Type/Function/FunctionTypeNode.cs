using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Type.Function;

public class FunctionTypeNode(
    PositionData positionData,
    List<FunctionTypeArgumentNode> parameters,
    TypeNode? returnType)
    : TypeNode(positionData), INodeAcceptor<ITypeNodeHandler>
{
    public List<FunctionTypeArgumentNode> Parameters { get; set; } = parameters;
    public TypeNode? ReturnType { get; set; } = returnType;

    public void Accept(ITypeNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not FunctionTypeNode node)
        {
            return false;
        }

        return node.Parameters.Count == Parameters.Count
               && Parameters.TestEquals(node.Parameters)
               && ReturnType.TestEqualsOrBothNull(node.ReturnType);
    }
}