using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Nodes.Type.Struct;

public class StructTypeNode(PosData posData, List<StructTypeFieldNode> fields) : TypeNode(posData)
{
    public List<StructTypeFieldNode> Fields { get; set; } = fields;

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