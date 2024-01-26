using ErrorReporting;
using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations.Enum;

namespace Syntax.Nodes.Declaration.Enum;

public class EnumCaseAssociatedValueNode(
    PositionData positionData,
    Nodes.IdentifierNode? name,
    Nodes.IdentifierNode type)
    : BaseNode(positionData), INodeAcceptor<IEnumChildNodeHandler>
{
    public Nodes.IdentifierNode? Name { get; set; } = name;
    public Nodes.IdentifierNode Type { get; set; } = type;

    public void Accept(IEnumChildNodeHandler handler)
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