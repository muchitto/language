using Syntax.Nodes.Declaration;

namespace Syntax.NodeHandlers.Declarations;

public interface IDeclarationBasicNodeHandler
{
    public void Handle(DeclarationNameNode declarationNameNode);
}