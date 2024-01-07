using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Statement;

public class ReturnNode(PositionData positionData, BaseNode? value) : StatementNode(positionData)
{
    public BaseNode? Value { get; set; } = value;

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

        Value?.TypeRefAdded();
    }
}