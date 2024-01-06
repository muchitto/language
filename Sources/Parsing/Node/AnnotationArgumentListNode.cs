using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Node;

public class AnnotationArgumentListNode(PosData posData, List<AnnotationArgumentNode> arguments) : BaseNode(posData)
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