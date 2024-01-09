using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Literal;

public class StructLiteralNode(PositionData positionData, List<StructLiteralFieldNode> fields)
    : LiteralNode(positionData)
{
    public List<StructLiteralFieldNode> Fields { get; set; } = fields;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not StructLiteralNode node)
        {
            return false;
        }

        return node.Fields.Count == Fields.Count && Fields.All(field => field.TestEquals(node));
    }
}