using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Type.Struct;

public class StructTypeNode(PositionData positionData, List<StructTypeFieldNode> fields) : TypeNode(positionData)
{
    public List<StructTypeFieldNode> Fields { get; set; } = fields;

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