using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes;

public class BodyContainerNode(PositionData positionData, List<BaseNode> statements, bool canReturn)
    : CodeBlockNode(positionData, statements), INodeAcceptor<ICodeBlockNodeHandler>
{
    public bool CanReturn { get; set; } = canReturn;


    public void Accept(ICodeBlockNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not BodyContainerNode node)
        {
            return false;
        }

        if (node.CanReturn != CanReturn)
        {
            return false;
        }

        return node.Statements.Count == Statements.Count && Statements.TestEquals(node.Statements);
    }
}