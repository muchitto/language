using Syntax.Nodes.Declaration.Function;
using Syntax.Nodes.Expression;

namespace Syntax.NodeHandlers.Declarations;

public interface IFunctionDeclarationNodeHandler
{
    public void Handle(FunctionDeclarationNode functionDeclarationNode);

    public void Handle(FunctionArgumentNode functionArgumentNode);

    public void Handle(FunctionCallNode functionCallNode);

    public void Handle(FunctionCallArgumentNode functionCallArgumentNode);

    public void Handle(ClosureNode closureNode);
}