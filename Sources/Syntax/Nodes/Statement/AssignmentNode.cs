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

    public override void SetTypeInfoFromTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name.SetTypeInfoFromTypeRef(typeRef);
    }
}