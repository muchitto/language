using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes;

public class AnnotationsNode(
    PositionData positionData,
    List<AnnotationNode> annotations) : BaseNode(positionData), INodeAcceptor<IAnnotationNodeHandler>
{
    public List<AnnotationNode> AnnotationNodes { get; set; } = annotations;

    public void Accept(IAnnotationNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        return other is AnnotationsNode node && AnnotationNodes.TestEquals(node.AnnotationNodes);
    }
}