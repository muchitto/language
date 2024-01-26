using Syntax.Nodes.Declaration.Function;

namespace Syntax.NodeHandlers.Declarations.Function;

public interface IFunctionChildNodeHandler
{
    public void Handle(FunctionArgumentNode functionArgumentNode);
}