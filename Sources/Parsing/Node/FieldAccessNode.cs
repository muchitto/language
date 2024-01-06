using Parsing.NodeHandlers;

namespace Parsing.Node;

public class FieldAccessNode(BaseNode left, BaseNode right) : BaseNode(left.PosData)
{
    public BaseNode Left { get; set; } = left;
    public BaseNode Right { get; set; } = right;

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

        Left.TypeRefAdded();
        Right.TypeRefAdded();
    }
}