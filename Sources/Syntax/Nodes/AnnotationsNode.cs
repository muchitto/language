using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes;

public class AnnotationsNode(
    PositionData positionData,
    List<AnnotationNode> annotations) : BaseNode(positionData)
{
    public List<AnnotationNode> Annotations { get; set; } = annotations;

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
        return other is AnnotationsNode node && Annotations.TestEquals(node.Annotations);
    }
}