using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Nodes.Type.Function;

public class FunctionTypeNode(PosData posData, List<FunctionTypeArgumentNode> parameters, TypeNode returnType)
    : TypeNode(posData)
{
    public List<FunctionTypeArgumentNode> Parameters { get; set; } = parameters;
    public TypeNode ReturnType { get; set; } = returnType;

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

        foreach (var parameter in Parameters)
        {
            parameter.TypeRefAdded();
        }

        ReturnType.TypeRefAdded();
    }
}