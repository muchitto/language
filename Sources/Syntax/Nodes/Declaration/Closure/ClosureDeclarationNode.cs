using ErrorReporting;
using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations.Function.Closure;

namespace Syntax.Nodes.Declaration.Closure;

public class ClosureDeclarationNode(
    PositionData positionData,
    List<ClosureArgumentNode> arguments,
    BodyContainerNode bodyContainerNode
) : DeclarationNode(positionData), INodeAcceptor<IClosureDeclarationNodeHandler>
{
    public List<ClosureArgumentNode> Arguments { get; } = arguments;
    public BodyContainerNode BodyContainerNode { get; } = bodyContainerNode;

    public void Accept(IClosureDeclarationNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        return other is ClosureDeclarationNode node && Arguments.TestEquals(node.Arguments);
    }
}