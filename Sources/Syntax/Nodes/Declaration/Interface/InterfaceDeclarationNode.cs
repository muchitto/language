using Syntax.NodeHandlers;
using Syntax.Nodes.Declaration.Function;
using TypeInformation;

namespace Syntax.Nodes.Declaration.Interface;

public class InterfaceDeclarationNode(
    IdentifierNode name,
    List<FunctionDeclarationNode> functions,
    List<VariableDeclarationNode> fields) : DeclarationNode(name)
{
    public List<FunctionDeclarationNode> Functions { get; set; } = functions;
    public List<VariableDeclarationNode> Fields { get; set; } = fields;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name.SetTypeRef(typeRef);
    }
}