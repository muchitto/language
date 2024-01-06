using Lexing;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Literal;

public class NumberLiteralNode(PosData posData, string value) : LiteralNode(posData)
{
    public string Value { get; set; } = value;

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