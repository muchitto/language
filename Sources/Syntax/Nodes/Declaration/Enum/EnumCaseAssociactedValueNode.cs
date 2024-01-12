using ErrorReporting;
using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations;

namespace Syntax.Nodes.Declaration.Enum;

public class EnumCaseAssociatedValueNode(
    PositionData positionData,
    IdentifierNode? name,
    IdentifierNode type)
    : BaseNode(positionData), INodeAcceptor<IEnumDeclarationNodeHandler>
{
    public IdentifierNode? Name { get; set; } = name;
    public IdentifierNode Type { get; set; } = type;

    public void Accept(IEnumDeclarationNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not EnumCaseAssociatedValueNode node)
        {
            return false;
        }

        return Name.TestEqualsOrBothNull(node.Name) && node.Type.TestEquals(Type);
    }
}