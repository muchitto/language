using Lexing;

namespace Parsing.Node;

public abstract class StatementListContainerNode(PosData posData, List<BaseNode> Statements) : BaseNode(posData)
{
    public List<BaseNode> Statements { get; set; } = Statements;
}