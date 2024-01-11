using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Declaration.Enum;

public class EnumDeclarationNode(IdentifierNode name, List<EnumCaseNode> cases, List<EnumFunctionNode> functions)
    : DeclarationNode(name)
{
    public List<EnumCaseNode> Cases { get; set; } = cases;

    public List<EnumFunctionNode> Functions { get; set; } = functions;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void PropagateTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name.PropagateTypeRef(typeRef);
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