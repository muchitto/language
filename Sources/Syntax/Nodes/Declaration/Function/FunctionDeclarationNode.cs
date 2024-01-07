using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Declaration.Function;

public class FunctionDeclarationNode(
    IdentifierNode name,
    List<FunctionArgumentNode> arguments,
    BodyContainerNode bodyContainerNode,
    bool canThrow,
    bool isMethod,
    IdentifierNode? returnTypeName)
    : DeclarationNode(name)
{
    public List<FunctionArgumentNode> Arguments { get; set; } = arguments;
    public BodyContainerNode BodyContainerNode { get; set; } = bodyContainerNode;

    public bool CanThrow { get; set; } = canThrow;
    public bool IsMethod { get; set; } = isMethod;
    public IdentifierNode? ReturnTypeName { get; set; } = returnTypeName;

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

        foreach (var argument in Arguments)
        {
            argument.TypeRefAdded();
        }

        BodyContainerNode.TypeRefAdded();
        ReturnTypeName?.TypeRefAdded();
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name.SetTypeRef(typeRef);
    }
}