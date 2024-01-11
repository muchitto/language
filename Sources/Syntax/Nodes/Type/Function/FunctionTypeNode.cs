using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Type.Function;

public class FunctionTypeNode(
    PositionData positionData,
    List<FunctionTypeArgumentNode> parameters,
    TypeNode? returnType)
    : TypeNode(positionData)
{
    public List<FunctionTypeArgumentNode> Parameters { get; set; } = parameters;
    public TypeNode? ReturnType { get; set; } = returnType;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }


    public override void PropagateTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not FunctionTypeNode node)
        {
            return false;
        }

        return node.Parameters.Count == Parameters.Count
               && Parameters.TestEquals(node.Parameters)
               && node.ReturnType.TestEquals(ReturnType);
    }

    public override TypeRef ResultingType()
    {
        return ReturnType?.TypeRef ?? TypeInformation.TypeRef.Void();
    }
}