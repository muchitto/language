using ErrorReporting;
using Syntax.NodeHandlers;
using Syntax.Nodes.Type;

namespace Syntax.Nodes;

public class IdentifierNode(PositionData positionData, string name)
    : BaseNode(positionData)
{
    public string Name { get; set; } = name;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public static explicit operator IdentifierNode(IdentifierTypeNode node)
    {
        return new IdentifierNode(node.PositionData, node.Name);
    }

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }
    }
}