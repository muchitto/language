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


    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name.SetTypeRef(typeRef);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not EnumCaseNode node)
        {
            return false;
        }

        return AssociatedValues.All(associatedValue =>
               {
                   return node.AssociatedValues.Any(x => x.TestEquals(associatedValue));
               })
               && node.Name.TestEquals(Name);
    }
}