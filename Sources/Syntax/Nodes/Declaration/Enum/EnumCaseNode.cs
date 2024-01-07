using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Declaration.Enum;

public class EnumCaseNode(
    PositionData positionData,
    IdentifierNode name,
    List<EnumCaseAssociatedValueNode> associatedValues)
    : BaseNode(positionData)
{
    public IdentifierNode Name { get; set; } = name;
    public List<EnumCaseAssociatedValueNode> AssociatedValues { get; set; } = associatedValues;

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

        Name.TypeRefAdded();

        foreach (var associatedValue in AssociatedValues)
        {
            associatedValue.TypeRefAdded();
        }
    }
}