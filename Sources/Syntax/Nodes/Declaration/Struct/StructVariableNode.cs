using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations;

namespace Syntax.Nodes.Declaration.Struct;

public class StructVariableNode(IdentifierNode name, VariableDeclarationNode variable)
    : StructFieldNode(name), INodeAcceptor<IStructDeclarationNodeHandler>
{
    public VariableDeclarationNode Variable { get; set; } = variable;

    public void Accept(IStructDeclarationNodeHandler handler)
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