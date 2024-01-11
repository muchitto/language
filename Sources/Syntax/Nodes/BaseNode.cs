using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes;

public abstract class BaseNode(PositionData positionData)
{
    public PositionData PositionData { get; set; } = positionData;

    public TypeRef TypeRef { get; protected set; }

    public AnnotationsNode? Annotations { get; set; } = null;

    public abstract void Accept(INodeHandler handler);

    public abstract void PropagateTypeRef(TypeRef typeRef);

    public abstract bool TestEquals(BaseNode other);
}