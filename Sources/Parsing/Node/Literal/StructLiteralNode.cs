using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Node.Literal;

public class StructLiteralNode(PosData posData, List<StructLiteralFieldNode> fields) : LiteralNode(posData)
{
    public List<StructLiteralFieldNode> Fields { get; set; } = fields;

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

        foreach (var field in Fields)
        {
            field.TypeRefAdded();
        }
    }
}