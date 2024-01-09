using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Type.Function;

public class FunctionTypeNode(PositionData positionData, List<FunctionTypeArgumentNode> parameters, TypeNode returnType)
    : TypeNode(positionData)
{
    public List<FunctionTypeArgumentNode> Parameters { get; set; } = parameters;
    public TypeNode ReturnType { get; set; } = returnType;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }


    public override void SetTypeRef(TypeRef typeRef)
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
               && Parameters.All(parameter => parameter.TestEquals(node))
               && node.ReturnType.TestEquals(ReturnType);
    }
}