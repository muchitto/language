using Parsing.Node.Declaration.Enum;

namespace Parsing.NodeHandlers.Declarations;

public interface IEnumDeclarationNodeHandler
{
    public void Handle(EnumDeclarationNode enumDeclarationNodeDeclaration);

    public void Handle(EnumFunctionNode enumFunctionNode);

    public void Handle(EnumCaseAssociatedValueNode enumCaseAssociatedValueNode);

    public void Handle(EnumCaseNode enumCaseNode);
}