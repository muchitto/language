using Parsing.Node.Declaration.Function;
using Parsing.Node.Expression;

namespace Parsing.NodeHandlers.Declarations;

public interface IFunctionDeclarationNodeHandler
{
    public void Handle(FunctionDeclarationNode functionDeclarationNode);

    public void Handle(FunctionArgumentNode functionArgumentNode);

    public void Handle(FunctionCallNode functionCallNode);

    public void Handle(FunctionCallArgumentNode functionCallArgumentNode);
}