using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

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


    public override void PropagateTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name.PropagateTypeRef(typeRef);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not EnumCaseNode node)
        {
            return false;
        }

        return AssociatedValues.TestEquals(node.AssociatedValues) && node.Name.TestEquals(Name);
    }
}