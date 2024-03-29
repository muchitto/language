using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes;

public class ProgramContainerNode(PositionData positionData, List<BaseNode> statements)
    : CodeBlockNode(positionData, statements), INodeAcceptor<ICodeBlockNodeHandler>
{
    public void Accept(ICodeBlockNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not ProgramContainerNode node)
        {
            return false;
        }

        return node.Statements.Count == Statements.Count && Statements.TestEquals(node.Statements);
    }
}