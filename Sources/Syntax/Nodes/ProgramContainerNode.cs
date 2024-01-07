using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes;

public class ProgramContainerNode(PositionData positionData, List<BaseNode> statements)
    : StatementListContainerNode(positionData, statements)
{
    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void TypeRefAdded()
    {
        foreach (var statement in Statements)
        {
            statement.TypeRefAdded();
        }
    }
}