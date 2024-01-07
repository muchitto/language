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


    public override void SetTypeInfoFromTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Type.SetTypeInfoFromTypeRef(typeRef);
    }
}