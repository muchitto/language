using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Node.Statement;

public class ReturnNode(PosData posData, BaseNode? value) : StatementNode(posData)
{
    public BaseNode? Value { get; set; } = value;

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

        Value?.TypeRefAdded();
    }
}