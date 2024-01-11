using ErrorReporting;
using Syntax.NodeHandlers;
using Syntax.Nodes.Declaration.Function;
using TypeInformation;

namespace Syntax.Nodes.Declaration.Struct;

public class StructFunctionNode(PositionData positionData, string name, FunctionDeclarationNode function)
    : StructFieldNode(positionData, name)
{
    public FunctionDeclarationNode Function { get; set; } = function;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void PropagateTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Function.PropagateTypeRef(typeRef);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not StructFunctionNode node)
        {
            return false;
        }

        return node.Function.TestEquals(Function) && node.Name == Name;
    }
}