using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations.Struct;

namespace Syntax.Nodes.Declaration.Struct;

public class StructVariableNode(DeclarationNameNode name, VariableDeclarationNode variable)
    : StructFieldNode(name), INodeAcceptor<IStructChildNodeHandler>
{
    public VariableDeclarationNode Variable { get; } = variable;

    public void Accept(IStructChildNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not StructVariableNode node)
        {
            return false;
        }

        return node.Variable.TestEquals(Variable) && node.Name == Name;
    }
}