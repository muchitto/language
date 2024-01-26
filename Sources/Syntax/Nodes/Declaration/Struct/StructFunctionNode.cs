using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations.Struct;
using Syntax.Nodes.Declaration.Function;

namespace Syntax.Nodes.Declaration.Struct;

public class StructFunctionNode(DeclarationNameNode name, FunctionDeclarationNode function)
    : StructFieldNode(name), INodeAcceptor<IStructChildNodeHandler>
{
    public FunctionDeclarationNode Function { get; set; } = function;

    public void Accept(IStructChildNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not StructFunctionNode node)
        {
            return false;
        }

        return node.Function.TestEquals(Function) && node.Name == Name;
    }
}