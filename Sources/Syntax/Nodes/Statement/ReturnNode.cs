using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Statement;

public class ReturnNode(PositionData positionData, BaseNode? value) : StatementNode(positionData)
{
    public BaseNode? Value { get; set; } = value;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Value?.SetTypeRef(typeRef);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not ReturnNode node)
        {
            return false;
        }

        return node.Value != null && (Value?.TestEquals(node.Value) ?? node.Value is null);
    }
}