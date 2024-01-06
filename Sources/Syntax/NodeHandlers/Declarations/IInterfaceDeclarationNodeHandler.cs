using Syntax.Nodes.Declaration.Interface;

namespace Syntax.NodeHandlers.Declarations;

public interface IInterfaceDeclarationNodeHandler
{
    public void Handle(InterfaceDeclarationNode interfaceDeclarationNodeDeclaration);
}