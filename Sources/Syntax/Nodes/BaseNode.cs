using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes;

public abstract class BaseNode(PositionData positionData)
{
    public PositionData PositionData { get; set; } = positionData;

    public TypeRef TypeRef { get; protected set; } 

    public abstract void Accept(INodeHandler handler);

    public abstract void SetTypeRef(TypeRef typeRef);
}