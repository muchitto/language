using ErrorReporting;

namespace Syntax.Nodes;

public abstract class BaseNode(PositionData positionData)
{
    public PositionData PositionData { get; set; } = positionData;

    public AnnotationsNode? Annotations { get; set; } = null;

    public abstract bool TestEquals(BaseNode other);
}