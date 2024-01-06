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
        SemanticContext.StartScope(ScopeType.Declaration);

        var fields = structDeclarationNode
            .Fields
            .ToDictionary(
                x => x.Name,
                x =>
                {
                    x.Accept(this);
                    return x.TypeRef;
                });

        var structType = new StructTypeInfo(fields);

        SemanticContext.EndScope();

        structDeclarationNode.TypeRef = DeclareType(structDeclarationNode.Name.Name, structType);
    }
}