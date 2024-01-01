using Parsing.Nodes;

namespace Parsing.NodeHandlers;

public interface IStructNodeHandler
{
    public void Handle(StructDeclarationNode structDeclarationNode);

    public void Handle(StructFieldNode structFieldNode);

    public void Handle(StructFunctionNode structFunctionNode);

    public void Handle(StructVariableNode structVariableNode);
}