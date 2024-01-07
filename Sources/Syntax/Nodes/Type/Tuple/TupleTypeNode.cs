using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Type.Tuple;

public class TupleTypeNode(PositionData positionData, List<TupleTypeFieldNode> types) : TypeNode(positionData)
{
    public List<TupleTypeFieldNode> Types { get; set; } = types;

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

        foreach (var type in Types)
        {
            type.TypeRefAdded();
        }
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
    }
}