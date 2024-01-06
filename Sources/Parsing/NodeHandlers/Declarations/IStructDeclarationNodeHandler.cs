using Parsing.Node.Declaration.Struct;

namespace Parsing.NodeHandlers.Declarations;

public interface IStructDeclarationNodeHandler
{
    public void Handle(StructDeclarationNode structDeclarationNode);

    public void Handle(StructFunctionNode structFunctionNode);

    public void Handle(StructVariableNode structVariableNode);
}