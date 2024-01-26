using Syntax.Nodes.Declaration.Enum;

namespace Syntax.NodeHandlers.Declarations.Enum;

public interface IEnumChildNodeHandler
{
    public void Handle(EnumFunctionNode enumFunctionNode);

    public void Handle(EnumCaseAssociatedValueNode enumCaseAssociatedValueNode);

    public void Handle(EnumCaseNode enumCaseNode);
}