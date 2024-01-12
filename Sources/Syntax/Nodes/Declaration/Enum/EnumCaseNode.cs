using ErrorReporting;
using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations;

namespace Syntax.Nodes.Declaration.Enum;

public class EnumCaseNode(
    PositionData positionData,
    IdentifierNode name,
    List<EnumCaseAssociatedValueNode> associatedValues)
    : BaseNode(positionData), INodeAcceptor<IEnumDeclarationNodeHandler>
{
    public IdentifierNode Name { get; set; } = name;
    public List<EnumCaseAssociatedValueNode> AssociatedValues { get; set; } = associatedValues;

    public void Accept(IEnumDeclarationNodeHandler handler)
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