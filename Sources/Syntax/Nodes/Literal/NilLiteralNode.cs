using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Literal;

public class NilLiteralNode(PositionData positionData)
    : LiteralNode(positionData), INodeAcceptor<ILiteralNodeHandler>
{
    public void Accept(ILiteralNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        return other is NilLiteralNode;
    }
}