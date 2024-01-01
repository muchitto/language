using Parsing.Nodes;

namespace Parsing.NodeHandlers;

public interface IInterfaceNodeHandler
{
    public void Handle(InterfaceDeclarationNode interfaceDeclarationNodeDeclaration);
}