using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations.Function.Closure;

namespace Syntax.Nodes.Declaration.Closure;

public class ClosureArgumentNode(IdentifierNode name, TypeNode? typeNode)
    : BaseNode(name.PositionData), INodeAcceptor<IClosureChildNodeHandler>
{
    public IdentifierNode Name { get; } = name;
    public TypeNode? TypeNode { get; } = typeNode;

    public void Accept(IClosureChildNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        return other is ClosureArgumentNode node && Name.TestEquals(node.Name);
    }
}