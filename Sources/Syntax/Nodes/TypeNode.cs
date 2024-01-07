using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes;

public abstract class TypeNode(PositionData positionData) : BaseNode(positionData)
{
    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}