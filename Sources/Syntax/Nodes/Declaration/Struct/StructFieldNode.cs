using ErrorReporting;

namespace Syntax.Nodes.Declaration.Struct;

public abstract class StructFieldNode(PositionData positionData, string name) : BaseNode(positionData)
{
    public string Name { get; set; } = name;
}