using Syntax.Nodes.Declaration.Interface;

namespace Syntax.NodeHandlers.Declarations.Interface;

public interface IInterfaceDeclarationNodeHandler
{
    public void Handle(InterfaceDeclarationNode interfaceDeclarationNodeDeclaration);
}