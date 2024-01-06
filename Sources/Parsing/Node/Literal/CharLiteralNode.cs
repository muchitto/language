using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Node.Literal;

public class CharLiteralNode(PosData posData, char value) : LiteralNode(posData)
{
    public char Value { get; set; } = value;

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