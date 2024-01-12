using Syntax.Nodes.Declaration;

namespace Syntax.NodeHandlers.Declarations;

public interface IVariableDeclarationHandler
{
    public void Handle(VariableDeclarationNode variableDeclarationNode);
}