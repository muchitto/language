using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Literal;

public class StructLiteralNode(PositionData positionData, List<StructLiteralFieldNode> fields)
    : LiteralNode(positionData)
{
    public List<StructLiteralFieldNode> Fields { get; set; } = fields;

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