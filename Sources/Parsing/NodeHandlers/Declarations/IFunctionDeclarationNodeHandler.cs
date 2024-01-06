using Parsing.Nodes;
using Parsing.Nodes.Declaration.Function;

namespace Parsing.NodeHandlers.Declarations;

public interface IFunctionDeclarationNodeHandler
{
    public void Handle(FunctionDeclarationNode functionDeclarationNode);

    public void Handle(FunctionArgumentNode functionArgumentNode);

    public void Handle(FunctionCallNode functionCallNode);

    public void Handle(FunctionCallArgumentNode functionCallArgumentNode);
}