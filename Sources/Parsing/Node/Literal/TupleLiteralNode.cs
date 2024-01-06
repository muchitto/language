using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Node.Literal;

public class TupleLiteralNode(PosData posData, List<BaseNode> values) : LiteralNode(posData)
{
    public List<BaseNode> Values { get; set; } = values;

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

        foreach (var value in Values)
        {
            value.TypeRefAdded();
        }
    }
}