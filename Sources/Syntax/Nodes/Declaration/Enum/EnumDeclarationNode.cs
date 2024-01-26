using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations.Enum;

namespace Syntax.Nodes.Declaration.Enum;

public class EnumDeclarationNode(
    DeclarationNameNode name,
    List<EnumCaseNode> cases,
    List<EnumFunctionNode> functions)
    : NamedDeclarationNode(name), INodeAcceptor<IEnumDeclarationNodeHandler>
{
    public List<EnumCaseNode> Cases { get; set; } = cases;

    public List<EnumFunctionNode> Functions { get; set; } = functions;

    public void Accept(IEnumDeclarationNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not EnumDeclarationNode node)
        {
            return false;
        }

        if (Cases.TestEquals(node.Cases))
        {
            return false;
        }

        return Functions.TestEquals(node.Functions) && node.Name.TestEquals(Name);
    }
}