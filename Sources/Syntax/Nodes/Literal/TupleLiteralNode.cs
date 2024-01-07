using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Literal;

public class TupleLiteralNode(PositionData positionData, List<BaseNode> values) : LiteralNode(positionData)
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