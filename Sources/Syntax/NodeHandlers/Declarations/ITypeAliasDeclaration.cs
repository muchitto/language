using Syntax.Nodes.Declaration;

namespace Syntax.NodeHandlers.Declarations;

public interface ITypeAliasDeclaration
{
    public void Handle(TypeAliasDeclarationNode typeAliasDeclarationNode);
}