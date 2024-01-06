using Lexing;
using Parsing.Node;
using Parsing.NodeHandlers;

namespace Parsing.Nodes.Type.Struct;

public class StructTypeFieldNode(PosData posData, string name, TypeNode type) : BaseNode(posData)
{
    public string Name { get; set; } = name;
    public TypeNode Type { get; set; } = type;

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

        Type.TypeRefAdded();
    }
}