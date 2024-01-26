using ErrorReporting;
using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations.Enum;

namespace Syntax.Nodes.Declaration.Enum;

public class EnumCaseNode(
    PositionData positionData,
    Nodes.IdentifierNode name,
    List<EnumCaseAssociatedValueNode> associatedValues)
    : BaseNode(positionData), INodeAcceptor<IEnumChildNodeHandler>
{
    public Nodes.IdentifierNode Name { get; set; } = name;
    public List<EnumCaseAssociatedValueNode> AssociatedValues { get; set; } = associatedValues;

    public void Accept(IEnumChildNodeHandler handler)
    {
        handler.Handle(this);
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