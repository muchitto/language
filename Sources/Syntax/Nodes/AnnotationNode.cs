using Syntax.NodeHandlers;

namespace Syntax.Nodes;

public class AnnotationNode(
    IdentifierNode name,
    List<AnnotationArgumentNode> arguments
)
    : BaseNode(name.PositionData), INodeAcceptor<IAnnotationNodeHandler>
{
    public IdentifierNode Name { get; set; } = name;
    public List<AnnotationArgumentNode> Arguments { get; set; } = arguments;

    public void Accept(IAnnotationNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not AnnotationNode node)
        {
            return false;
        }

        if (!Name.TestEquals(node.Name))
        {
            return false;
        }

        return Arguments.TestEquals(node.Arguments);
    }
}