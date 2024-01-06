using Syntax.Nodes.Declaration.Struct;
using TypeInformation;

namespace Semantics.Passes.DeclarationPass;

public partial class DeclarationPass
{
    public void Handle(StructFunctionNode structFunctionNode)
    {
        structFunctionNode.Function.Accept(this);

        structFunctionNode.TypeRef = structFunctionNode.Function.TypeRef;
    }

    public void Handle(StructVariableNode structVariableNode)
    {
        structVariableNode.Variable.Accept(this);

        structVariableNode.TypeRef = structVariableNode.Variable.TypeRef;
    }

    public void Handle(StructDeclarationNode structDeclarationNode)
    {
        structDeclarationNode.Fields.ForEach(field =>
        {
            if (field is StructVariableNode variable)
            {
                variable.Variable.Type?.Accept(this);
            }
            else if (field is StructFunctionNode function)
            {
                function.Function.ReturnTypeName?.Accept(this);
            }
            else
            {
                throw new Exception("Unknown struct field type");
            }
        });

        SemanticContext.StartScope(ScopeType.Declaration);

        var fields = structDeclarationNode
            .Fields
            .ToDictionary(
                x => x.Name,
                x => x.TypeRef
            );

        var structType = new StructTypeInfo(fields);

        SemanticContext.EndScope();

        structDeclarationNode.TypeRef = DeclareType(structDeclarationNode.Name.Name, structType);
    }
}