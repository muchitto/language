using ErrorReporting;
using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations;

namespace Syntax.Nodes.Declaration.Closure;

public class ClosureNode(
    PositionData positionData,
    List<ClosureArgumentNode> arguments,
    BodyContainerNode bodyContainerNode
) : BaseNode(positionData), INodeAcceptor<IClosureDeclarationNodeHandler>
{
    public List<ClosureArgumentNode> Arguments { get; } = arguments;
    public BodyContainerNode BodyContainerNode { get; } = bodyContainerNode;

    public void Accept(IClosureDeclarationNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        return other is ClosureNode node && Arguments.TestEquals(node.Arguments);
    }
}