using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Node;

public class BodyContainerNode(PosData posData, List<BaseNode> statements, bool canReturn)
    : StatementListContainerNode(posData, statements)
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