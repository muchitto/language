using Syntax.Nodes.Declaration.Enum;

namespace Syntax.NodeHandlers.Declarations.Enum;

public interface IEnumDeclarationNodeHandler
{
    public void Handle(EnumDeclarationNode enumDeclarationNodeDeclaration);
}