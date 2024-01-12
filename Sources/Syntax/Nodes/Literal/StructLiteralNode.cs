using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Literal;

public class StructLiteralNode(PositionData positionData, List<StructLiteralFieldNode> fields)
    : LiteralNode(positionData), INodeAcceptor<ILiteralNodeHandler>
{
    public List<StructLiteralFieldNode> Fields { get; set; } = fields;

    public void Accept(ILiteralNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not StructLiteralNode node)
        {
            return false;
        }

        return node.Fields.Count == Fields.Count && Fields.TestEquals(node.Fields);
    }
}