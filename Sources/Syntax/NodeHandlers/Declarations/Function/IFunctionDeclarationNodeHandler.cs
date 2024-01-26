using Syntax.Nodes.Declaration.Function;

namespace Syntax.NodeHandlers.Declarations.Function;

public interface IFunctionDeclarationNodeHandler
{
    public void Handle(FunctionDeclarationNode functionDeclarationNode);
}