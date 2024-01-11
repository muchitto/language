using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes;

public class AnnotationNode(
    IdentifierNode name,
    List<AnnotationArgumentNode> arguments
)
    : BaseNode(name.PositionData)
{
    public IdentifierNode Name { get; set; } = name;
    public List<AnnotationArgumentNode> Arguments { get; set; } = arguments;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void PropagateTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name.PropagateTypeRef(typeRef);
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