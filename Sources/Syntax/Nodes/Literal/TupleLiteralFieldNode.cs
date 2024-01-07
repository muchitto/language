using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Literal;

public class TupleLiteralFieldNode(PositionData positionData, string? name, BaseNode value) : LiteralNode(positionData)
{
    public BaseNode Value { get; set; } = value;
    public string? Name { get; set; } = name;

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

        Value.TypeRefAdded();
    }
}