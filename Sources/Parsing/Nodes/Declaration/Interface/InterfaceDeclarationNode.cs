using Parsing.NodeHandlers;
using Parsing.Nodes.Declaration.Function;

namespace Parsing.Nodes.Declaration.Interface;

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

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }

        Name.TypeRefAdded();

        foreach (var function in Functions)
        {
            function.TypeRefAdded();
        }

        foreach (var field in Fields)
        {
            field.TypeRefAdded();
        }
    }
}