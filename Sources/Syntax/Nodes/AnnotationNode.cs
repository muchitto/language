using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes;

public class AnnotationNode(
    IdentifierNode name,
    AnnotationArgumentListNode arguments,
    BaseNode attachedNode)
    : BaseNode(name.PositionData)
{
    public IdentifierNode Name { get; set; } = name;
    public AnnotationArgumentListNode Arguments { get; set; } = arguments;
    public BaseNode AttachedNode { get; set; } = attachedNode;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name.SetTypeRef(typeRef);
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

        return Arguments.TestEquals(node.Arguments) && AttachedNode.TestEquals(node.AttachedNode);
    }
}