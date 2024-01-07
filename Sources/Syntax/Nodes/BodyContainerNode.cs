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

    public override void SetTypeInfoFromTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;

        foreach (var statement in Statements)
        {
            statement.SetTypeInfoFromTypeRef(typeRef);
        }
    }
}