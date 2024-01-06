using Syntax.Nodes.Declaration.Struct;

namespace Syntax.NodeHandlers.Declarations;

public interface IStructDeclarationNodeHandler
{
    public void Handle(StructDeclarationNode structDeclarationNode);

    public void Handle(StructFunctionNode structFunctionNode);

    public void Handle(StructVariableNode structVariableNode);
}