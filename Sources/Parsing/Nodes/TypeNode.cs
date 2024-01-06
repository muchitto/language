using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Nodes;

public abstract class TypeNode(PosData posData) : BaseNode(posData)
{
    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}