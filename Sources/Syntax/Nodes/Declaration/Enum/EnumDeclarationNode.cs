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

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name.SetTypeRef(typeRef);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not EnumDeclarationNode node)
        {
            return false;
        }

        if (Cases.Any(@case => !node.Cases.Any(x => x.TestEquals(@case))))
        {
            return false;
        }

        if (Functions.Any(function => !node.Functions.Any(x => x.TestEquals(function))))
        {
            return false;
        }

        return node.Name.TestEquals(Name);
    }
}