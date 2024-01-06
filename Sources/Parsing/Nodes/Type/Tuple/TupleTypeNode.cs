using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Nodes.Type.Tuple;

public class TupleTypeNode(PosData posData, List<TupleTypeFieldNode> types) : TypeNode(posData)
{
    public List<TupleTypeFieldNode> Types { get; set; } = types;

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

        foreach (var type in Types)
        {
            type.TypeRefAdded();
        }
    }
}