using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Type.Tuple;

public class TupleTypeNode(PositionData positionData, List<TupleTypeFieldNode> types)
    : TypeNode(positionData), INodeAcceptor<ITypeNodeHandler>
{
    public List<TupleTypeFieldNode> Types { get; set; } = types;

    public void Accept(ITypeNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not TupleTypeNode node)
        {
            return false;
        }

        return node.Types.Count == Types.Count && Types.TestEquals(node.Types);
    }
}