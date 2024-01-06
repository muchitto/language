using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Node.Declaration.Enum;

public class EnumCaseAssociatedValueNode(PosData posData, IdentifierNode? name, IdentifierNode type) : BaseNode(posData)
{
    public IdentifierNode? Name { get; set; } = name;
    public IdentifierNode Type { get; set; } = type;

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

        Name?.TypeRefAdded();
        Type.TypeRefAdded();
    }
}