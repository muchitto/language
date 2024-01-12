using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Literal;

public class StructLiteralFieldNode(PositionData positionData, IdentifierNode name, BaseNode value)
    : LiteralNode(positionData), INodeAcceptor<ILiteralNodeHandler>
{
    public IdentifierNode Name { get; set; } = name;
    public BaseNode Value { get; set; } = value;

    public void Accept(ILiteralNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not StructLiteralFieldNode node)
        {
            return false;
        }

        return node.Name.TestEquals(Name) && node.Value.TestEquals(Value);
    }
}