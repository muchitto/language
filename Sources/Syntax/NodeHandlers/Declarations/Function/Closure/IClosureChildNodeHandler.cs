using Syntax.Nodes.Declaration.Closure;

namespace Syntax.NodeHandlers.Declarations.Function.Closure;

public interface IClosureChildNodeHandler
{
    public void Handle(ClosureArgumentNode closureArgumentNode);
}