using Lexing;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Literal;

public class StructLiteralFieldNode(PosData posData, IdentifierNode name, BaseNode value) : LiteralNode(posData)
{
    public IdentifierNode Name { get; set; } = name;
    public BaseNode Value { get; set; } = value;

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

        Name.TypeRefAdded();
        Value.TypeRefAdded();
    }
}