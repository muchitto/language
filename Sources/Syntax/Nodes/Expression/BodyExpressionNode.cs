using Lexing;
using Syntax.NodeHandlers;

namespace Syntax.Nodes.Expression;

public class BodyExpressionNode(PosData posData, List<BaseNode> statements) : ExpressionNode(posData)
{
    public List<BaseNode> Statements { get; set; } = statements;

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