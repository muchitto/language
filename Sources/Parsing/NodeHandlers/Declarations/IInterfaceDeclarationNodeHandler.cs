using Parsing.Nodes.Declaration.Interface;

namespace Parsing.NodeHandlers.Declarations;

public interface IInterfaceDeclarationNodeHandler
{
    public void Handle(InterfaceDeclarationNode interfaceDeclarationNodeDeclaration);
}