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

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not ProgramContainerNode node)
        {
            return false;
        }

        if (node.Statements.Count != Statements.Count)
        {
            return false;
        }

        for (var i = 0; i < Statements.Count; i++)
        {
            if (!Statements[i].TestEquals(node.Statements[i]))
            {
                return false;
            }
        }

        return true;
    }
}