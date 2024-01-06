using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Node;

public class AnnotationArgumentNode(PosData posData, IdentifierNode name, ExpressionNode value)
    : BaseNode(posData)
{
    public IdentifierNode Name { get; set; } = name;
    public ExpressionNode Value { get; set; } = value;

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

        Name.TypeRefAdded();
        Value.TypeRefAdded();
    }
}