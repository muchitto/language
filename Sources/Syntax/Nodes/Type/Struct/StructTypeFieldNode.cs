using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Type.Struct;

public class StructTypeFieldNode(PositionData positionData, string name, TypeNode type) : BaseNode(positionData)
{
    public string Name { get; set; } = name;
    public TypeNode Type { get; set; } = type;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }


    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Type.SetTypeRef(typeRef);
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