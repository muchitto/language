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


    public override void SetTypeInfoFromTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
    }
}