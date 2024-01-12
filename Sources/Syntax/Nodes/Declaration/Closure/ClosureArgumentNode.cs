using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations;

namespace Syntax.Nodes.Declaration.Closure;

public class ClosureArgumentNode(IdentifierNode name, TypeNode? typeNode)
    : BaseNode(name.PositionData), INodeAcceptor<IClosureDeclarationNodeHandler>
{
    public IdentifierNode Name { get; } = name;
    public TypeNode? TypeNode { get; } = typeNode;

    public void Accept(IClosureDeclarationNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        return other is ClosureArgumentNode node && Name.TestEquals(node.Name);
    }
}