using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Declaration.Closure;

public class ClosureArgumentNode(IdentifierNode name, TypeNode? typeNode)
    : BaseNode(name.PositionData)
{
    public IdentifierNode Name { get; } = name;
    public TypeNode? TypeNode { get; } = typeNode;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;

        TypeNode?.SetTypeRef(typeRef);
    }

    public override bool TestEquals(BaseNode other)
    {
        return other is ClosureArgumentNode node && Name.TestEquals(node.Name);
    }
}