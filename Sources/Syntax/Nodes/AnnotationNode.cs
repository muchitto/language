using Syntax.NodeHandlers;

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

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }

        Name.TypeRefAdded();
        Arguments.TypeRefAdded();
        AttachedNode.TypeRefAdded();
    }
}