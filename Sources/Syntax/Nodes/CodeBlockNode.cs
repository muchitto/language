using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes;

public class CodeBlockNode(PositionData positionData, List<BaseNode> statements)
    : BaseNode(positionData), INodeAcceptor<ICodeBlockNodeHandler>
{
    public List<BaseNode> Statements { get; set; } = statements;

    public void Accept(ICodeBlockNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not CodeBlockNode node)
        {
            return false;
        }

        return node.Statements.Count == Statements.Count && Statements.TestEquals(node.Statements);
    }
}