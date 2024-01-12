using ErrorReporting;

namespace Syntax.Nodes;

public abstract class CodeBlockNode(PositionData positionData, List<BaseNode> statements)
    : BaseNode(positionData)
{
    public List<BaseNode> Statements { get; set; } = statements;
}