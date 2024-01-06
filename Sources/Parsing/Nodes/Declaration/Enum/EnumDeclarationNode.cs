using Parsing.NodeHandlers;

namespace Parsing.Nodes.Declaration.Enum;

public class EnumDeclarationNode(IdentifierNode name, List<EnumCaseNode> cases, List<EnumFunctionNode> functions)
    : DeclarationNode(name)
{
    public List<EnumCaseNode> Cases { get; set; } = cases;

    public List<EnumFunctionNode> Functions { get; set; } = functions;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }

        Name.TypeRefAdded();

        foreach (var @case in Cases)
        {
            @case.TypeRefAdded();
        }

        foreach (var function in Functions)
        {
            function.TypeRefAdded();
        }
    }
}