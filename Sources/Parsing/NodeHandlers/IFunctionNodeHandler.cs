using Parsing.Nodes;

namespace Parsing.NodeHandlers;

public interface IFunctionNodeHandler
{
    public void HandleStart(FunctionDeclarationNode functionDeclarationNode);
    public void HandleEnd(FunctionDeclarationNode functionDeclarationNode);

    public void HandleStart(FunctionArgumentListNode functionArgumentListNode);
    public void HandleEnd(FunctionArgumentListNode functionArgumentListNode);

    public void Handle(FunctionArgumentNode functionArgumentNode);

    public void Handle(FunctionCallNode functionCallNode);
    public void Handle(FunctionCallArgumentNode functionCallArgumentNode);
}