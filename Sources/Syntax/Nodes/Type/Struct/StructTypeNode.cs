using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Type.Struct;

public class StructTypeNode(PositionData positionData, List<StructTypeFieldNode> fields) : TypeNode(positionData)
{
    public List<StructTypeFieldNode> Fields { get; set; } = fields;

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
        if (other is not StructTypeNode node)
        {
            return false;
        }

        return node.Fields.Count == Fields.Count && Fields.All(field => field.TestEquals(node));
    }
}