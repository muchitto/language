using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations.Interface;
using Syntax.Nodes.Declaration.Function;

namespace Syntax.Nodes.Declaration.Interface;

public class InterfaceDeclarationNode(
    DeclarationNameNode name,
    List<FunctionDeclarationNode> functions,
    List<VariableDeclarationNode> fields)
    : NamedDeclarationNode(name), INodeAcceptor<IInterfaceDeclarationNodeHandler>
{
    public List<FunctionDeclarationNode> Functions { get; set; } = functions;
    public List<VariableDeclarationNode> Fields { get; set; } = fields;

    public void Accept(IInterfaceDeclarationNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not InterfaceDeclarationNode node)
        {
            return false;
        }

        if (Functions.Any(function => !node.Functions.Any(x => x.TestEquals(function))))
        {
            return false;
        }

        return Fields.TestEquals(node.Fields)
               && node.Name.TestEquals(Name);
    }
}