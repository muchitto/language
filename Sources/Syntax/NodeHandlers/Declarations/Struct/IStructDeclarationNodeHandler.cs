using Syntax.Nodes.Declaration.Struct;

namespace Syntax.NodeHandlers.Declarations.Struct;

public interface IStructDeclarationNodeHandler
{
    public void Handle(StructDeclarationNode structDeclarationNode);
}