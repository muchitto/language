using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Type.Struct;

public class StructTypeNode(PositionData positionData, List<StructTypeFieldNode> fields)
    : TypeNode(positionData), INodeAcceptor<ITypeNodeHandler>
{
    public List<StructTypeFieldNode> Fields { get; set; } = fields;

    public void Accept(ITypeNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not StructTypeNode node)
        {
            return false;
        }

        return node.Fields.Count == Fields.Count && Fields.TestEquals(node.Fields);
    }
}