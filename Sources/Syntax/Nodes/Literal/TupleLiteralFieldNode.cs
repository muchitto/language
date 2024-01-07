using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Literal;

public class TupleLiteralFieldNode(PositionData positionData, string? name, BaseNode value) : LiteralNode(positionData)
{
    public BaseNode Value { get; set; } = value;
    public string? Name { get; set; } = name;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
    }
}