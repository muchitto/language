using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Declaration.Closure;

public class ClosureNode(
    PositionData positionData,
    List<ClosureArgumentNode> arguments,
    BodyContainerNode bodyContainerNode
) : BaseNode(positionData)
{
    public List<ClosureArgumentNode> Arguments { get; } = arguments;
    public BodyContainerNode BodyContainerNode { get; } = bodyContainerNode;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void PropagateTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
    }

    public override bool TestEquals(BaseNode other)
    {
        return other is ClosureNode node && Arguments.TestEquals(node.Arguments);
    }
}