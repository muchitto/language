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


    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Variable.SetTypeRef(typeRef);
    }
}