using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Declaration.Enum;

public class EnumCaseAssociatedValueNode(PositionData positionData, IdentifierNode? name, IdentifierNode type)
    : BaseNode(positionData)
{
    public IdentifierNode? Name { get; set; } = name;
    public IdentifierNode Type { get; set; } = type;

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

        Name?.TypeRefAdded();
        Type.TypeRefAdded();
    }
}