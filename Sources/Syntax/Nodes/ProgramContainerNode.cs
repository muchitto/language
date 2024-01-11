using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes;

public class ProgramContainerNode(PositionData positionData, List<BaseNode> statements)
    : StatementListContainerNode(positionData, statements)
{
    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void PropagateTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
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