using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Statement;

public class AssignmentNode(BaseNode name, BaseNode value) : StatementNode(name.PositionData)
{
    public BaseNode Name { get; set; } = name;
    public BaseNode Value { get; set; } = value;

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
        if (other is not AssignmentNode node)
        {
            return false;
        }

        return node.Name.TestEquals(Name) && node.Value.TestEquals(Value);
    }
}