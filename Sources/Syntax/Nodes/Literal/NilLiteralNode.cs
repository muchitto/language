using Lexing;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Literal;

public class NilLiteralNode(PosData posData) : LiteralNode(posData)
{
    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }
    }
}