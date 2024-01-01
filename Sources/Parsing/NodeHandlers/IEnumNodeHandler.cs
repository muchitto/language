using Parsing.Nodes;

namespace Parsing.NodeHandlers;

public interface IEnumNodeHandler
{
    public void Handle(EnumNode enumNodeDeclaration);

    public void Handle(EnumFunctionNode enumFunctionNode);

    public void Handle(EnumCaseAssociatedValueNode enumCaseAssociatedValueNode);

    public void Handle(EnumCaseNode enumCaseNode);
}