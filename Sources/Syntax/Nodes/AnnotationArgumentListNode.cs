using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes;

public class AnnotationArgumentListNode(PositionData positionData, List<AnnotationArgumentNode> arguments)
    : BaseNode(positionData)
{
    public List<AnnotationArgumentNode> Arguments { get; set; } = arguments;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        foreach (var argument in Arguments)
        {
            argument.SetTypeRef(typeRef);
        }
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not AnnotationArgumentListNode node)
        {
            return false;
        }

        return node.Arguments.Count == Arguments.Count && Arguments.TestEquals(node.Arguments);
    }
}