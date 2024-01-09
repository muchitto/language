using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes;

public class AnnotationArgumentNode(PositionData positionData, IdentifierNode name, ExpressionNode value)
    : BaseNode(positionData)
{
    public IdentifierNode Name { get; set; } = name;
    public ExpressionNode Value { get; set; } = value;

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
        if (other is not AnnotationArgumentNode node)
        {
            return false;
        }

        return Name.TestEquals(node.Name) && Value.TestEquals(node.Value);
    }
}