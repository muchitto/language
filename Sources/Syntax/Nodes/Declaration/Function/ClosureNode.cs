using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Declaration.Function;

public class ClosureNode(
    PositionData positionData,
    List<FunctionArgumentNode> arguments,
    BodyContainerNode bodyContainerNode) : BaseNode(positionData)
{
    public List<FunctionArgumentNode> Arguments { get; } = arguments;
    public BodyContainerNode BodyContainerNode { get; } = bodyContainerNode;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
    }

    public override bool TestEquals(BaseNode other)
    {
        return other is ClosureNode node && Arguments.TestEquals(node.Arguments);
    }
}