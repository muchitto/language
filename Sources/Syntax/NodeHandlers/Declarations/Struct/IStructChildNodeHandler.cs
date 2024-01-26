using Syntax.Nodes.Declaration.Struct;

namespace Syntax.NodeHandlers.Declarations.Struct;

public interface IStructChildNodeHandler
{
    public void Handle(StructFunctionNode structFunctionNode);

    public void Handle(StructVariableNode structVariableNode);
}