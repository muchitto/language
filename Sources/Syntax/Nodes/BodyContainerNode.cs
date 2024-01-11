using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes;

public class BodyContainerNode(PositionData positionData, List<BaseNode> statements, bool canReturn)
    : StatementListContainerNode(positionData, statements)
{
    public bool CanReturn { get; set; } = canReturn;


    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void PropagateTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;

        foreach (var statement in Statements)
        {
            statement.PropagateTypeRef(typeRef);
        }
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