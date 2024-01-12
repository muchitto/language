using Syntax.Nodes.Declaration.Function;

namespace Syntax.NodeHandlers.Declarations;

public interface IFunctionDeclarationNodeHandler
{
    public void Handle(FunctionDeclarationNode functionDeclarationNode);

    public void Handle(FunctionArgumentNode functionArgumentNode);
}