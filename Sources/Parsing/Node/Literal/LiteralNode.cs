using Lexing;

namespace Parsing.Node.Literal;

public abstract class LiteralNode : BaseNode
{
    protected LiteralNode(PosData posData) : base(posData)
    {
    }
}