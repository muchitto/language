using Syntax.Nodes.Declaration.Closure;

namespace Syntax.NodeHandlers.Declarations.Function.Closure;

public interface IClosureDeclarationNodeHandler
{
    public void Handle(ClosureDeclarationNode closureDeclarationNode);
}