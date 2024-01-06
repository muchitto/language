using Parsing.NodeHandlers;

namespace Parsing.Node.Statement;

public class AssignmentNode(BaseNode name, BaseNode value) : StatementNode(name.PosData)
{
    public BaseNode Name { get; set; } = name;
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

        Name.TypeRefAdded();
        Value.TypeRefAdded();
    }
}