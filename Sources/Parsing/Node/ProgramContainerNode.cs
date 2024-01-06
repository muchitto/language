using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Node;

public class ProgramContainerNode(PosData posData, List<BaseNode> statements)
    : StatementListContainerNode(posData, statements)
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