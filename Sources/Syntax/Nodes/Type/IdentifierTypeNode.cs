using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Type;

public class IdentifierTypeNode(PositionData positionData, string name) : TypeNode(positionData)
{
    public string Name { get; set; } = name;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public static explicit operator IdentifierTypeNode(IdentifierNode node)
    {
        return new IdentifierTypeNode(node.PositionData, node.Name);
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
    }
}