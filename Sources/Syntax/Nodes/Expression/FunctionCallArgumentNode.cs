using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Expression;

public class FunctionCallArgumentNode(PositionData positionData, IdentifierNode? name, BaseNode value)
    : BaseNode(positionData)
{
    public IdentifierNode? Name { get; set; } = name;
    public BaseNode Value { get; set; } = value;

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

        Value.TypeRefAdded();
        Name?.TypeRefAdded();
    }
}