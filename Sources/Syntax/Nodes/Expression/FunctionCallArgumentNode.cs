using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

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

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name?.SetTypeRef(typeRef);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not FunctionCallArgumentNode node)
        {
            return false;
        }

        return Name.TestEqualsOrBothNull(node.Name) && node.Value.TestEquals(Value);
    }
}