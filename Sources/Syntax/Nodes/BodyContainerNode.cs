using ErrorReporting;
using Syntax.NodeHandlers;

namespace Syntax.Nodes;

public class BodyContainerNode(PositionData positionData, List<BaseNode> statements, bool canReturn)
    : StatementListContainerNode(positionData, statements)
{
    public bool CanReturn { get; set; } = canReturn;


    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }

        foreach (var statement in Statements)
        {
            statement.TypeRefAdded();
        }
    }
}