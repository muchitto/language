using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes;

public class AnnotationArgumentListNode(PositionData positionData, List<AnnotationArgumentNode> arguments)
    : BaseNode(positionData)
{
    public List<AnnotationArgumentNode> Arguments { get; set; } = arguments;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }

        foreach (var argument in Arguments)
        {
            argument.TypeRefAdded();
        }
    }
}