using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Node.Literal;

public class TupleLiteralFieldNode(PosData posData, string? name, BaseNode value) : LiteralNode(posData)
{
    public BaseNode Value { get; set; } = value;
    public string? Name { get; set; } = name;

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

        Value.TypeRefAdded();
    }
}