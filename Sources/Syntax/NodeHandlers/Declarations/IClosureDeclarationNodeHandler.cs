using Syntax.Nodes.Declaration.Closure;

namespace Syntax.NodeHandlers.Declarations;

public interface IClosureDeclarationNodeHandler
{
    public void Handle(ClosureNode closureNode);

    public void Handle(ClosureArgumentNode closureArgumentNode);
}