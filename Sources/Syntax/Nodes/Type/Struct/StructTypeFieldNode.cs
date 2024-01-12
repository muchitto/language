using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Type.Struct;

public class StructTypeFieldNode(PositionData positionData, string name, TypeNode type)
    : TypeNode(positionData), INodeAcceptor<ITypeNodeHandler>
{
    public string Name { get; set; } = name;
    public TypeNode Type { get; set; } = type;

    public void Accept(ITypeNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not StructTypeFieldNode node)
        {
            return false;
        }

        return node.Name == Name && node.Type.TestEquals(Type);
    }
}