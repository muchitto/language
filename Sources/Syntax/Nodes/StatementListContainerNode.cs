using ErrorReporting;

namespace Syntax.Nodes;

public abstract class StatementListContainerNode(PositionData positionData, List<BaseNode> Statements)
    : BaseNode(positionData)
{
    public List<BaseNode> Statements { get; set; } = Statements;
}