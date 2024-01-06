using Lexing;
using Syntax.NodeHandlers;

namespace Syntax.Nodes;

public abstract class TypeNode(PosData posData) : BaseNode(posData)
{
    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}