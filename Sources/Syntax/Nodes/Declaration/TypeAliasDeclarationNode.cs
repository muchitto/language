using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations;

namespace Syntax.Nodes.Declaration;

public class TypeAliasDeclarationNode(IdentifierNode name, TypeNode type)
    : DeclarationNode(name), INodeAcceptor<ITypeAliasDeclaration>
{
    public TypeNode Type { get; set; } = type;

    public void Accept(ITypeAliasDeclaration handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not TypeAliasDeclarationNode node)
        {
            return false;
        }

        return node.Type.TestEquals(Type) && node.Name.TestEquals(Name);
    }
}