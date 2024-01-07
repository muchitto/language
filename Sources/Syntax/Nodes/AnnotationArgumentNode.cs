using ErrorReporting;
using Syntax.NodeHandlers;

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

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }

        Name.TypeRefAdded();
        Value.TypeRefAdded();
    }
}