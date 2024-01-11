using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Declaration.Struct;

public class StructVariableNode(PositionData positionData, string name, VariableDeclarationNode variable)
    : StructFieldNode(positionData, name)
{
    public VariableDeclarationNode Variable { get; set; } = variable;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }


    public override void PropagateTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Variable.PropagateTypeRef(typeRef);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not StructVariableNode node)
        {
            return false;
        }

        return node.Variable.TestEquals(Variable) && node.Name == Name;
    }
}