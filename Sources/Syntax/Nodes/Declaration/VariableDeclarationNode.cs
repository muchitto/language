using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations;

namespace Syntax.Nodes.Declaration;

public class VariableDeclarationNode(
    IdentifierNode name,
    BaseNode? value,
    bool isLet,
    TypeNode? typeNode,
    bool isDynamic
)
    : DeclarationNode(name), INodeAcceptor<IVariableDeclarationHandler>
{
    public BaseNode? Value { get; set; } = value;
    public bool IsLet { get; set; } = isLet;
    public TypeNode? Type { get; set; } = typeNode;
    public bool IsDynamic { get; set; } = isDynamic;

    public void Accept(IVariableDeclarationHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not VariableDeclarationNode node)
        {
            return false;
        }

        return node.IsLet == IsLet
               && node.IsDynamic == IsDynamic
               && Value.TestEqualsOrBothNull(node.Value)
               && Type.TestEqualsOrBothNull(node.Type)
               && node.Name.TestEquals(Name);
    }
}