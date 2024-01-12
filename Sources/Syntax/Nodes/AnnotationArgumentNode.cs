using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes;

public class AnnotationArgumentNode(
    PositionData positionData,
    IdentifierNode name,
    ExpressionNode value)
    : BaseNode(positionData), INodeAcceptor<IAnnotationNodeHandler>
{
    public IdentifierNode Name { get; set; } = name;
    public ExpressionNode Value { get; set; } = value;

    public void Accept(IAnnotationNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not AnnotationArgumentNode node)
        {
            return false;
        }

        return Name.TestEquals(node.Name) && Value.TestEquals(node.Value);
    }
}