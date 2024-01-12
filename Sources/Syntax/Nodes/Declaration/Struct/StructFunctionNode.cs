using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations;
using Syntax.Nodes.Declaration.Function;

namespace Syntax.Nodes.Declaration.Struct;

public class StructFunctionNode(IdentifierNode name, FunctionDeclarationNode function)
    : StructFieldNode(name), INodeAcceptor<IStructDeclarationNodeHandler>
{
    public FunctionDeclarationNode Function { get; set; } = function;

    public void Accept(IStructDeclarationNodeHandler handler)
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