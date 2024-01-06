using Lexing;
using Parsing.Node;
using Parsing.NodeHandlers;

namespace Parsing.Nodes.Type;

public class IdentifierTypeNode(PosData posData, string name) : TypeNode(posData)
{
    public string Name { get; set; } = name;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public static explicit operator IdentifierTypeNode(IdentifierNode node)
    {
        return new IdentifierTypeNode(node.PosData, node.Name);
    }

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }
    }
}