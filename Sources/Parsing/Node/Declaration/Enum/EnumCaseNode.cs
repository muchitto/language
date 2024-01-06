using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Node.Declaration.Enum;

public class EnumCaseNode(PosData posData, IdentifierNode name, List<EnumCaseAssociatedValueNode> associatedValues)
    : BaseNode(posData)
{
    public IdentifierNode Name { get; set; } = name;
    public List<EnumCaseAssociatedValueNode> AssociatedValues { get; set; } = associatedValues;

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

        foreach (var associatedValue in AssociatedValues)
        {
            associatedValue.TypeRefAdded();
        }
    }
}