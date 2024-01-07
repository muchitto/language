using Syntax.Nodes.Declaration.Struct;
using TypeInformation;

namespace Semantics.Passes.DeclarationPass;

public partial class DeclarationPass
{
    public void Handle(StructFunctionNode structFunctionNode)
    {
        AddNodeToScope(structFunctionNode);

        structFunctionNode.Function.Accept(this);
        structFunctionNode.SetTypeRef(structFunctionNode.Function.TypeRef);
    }

    public void Handle(StructVariableNode structVariableNode)
    {
        AddNodeToScope(structVariableNode);

        structVariableNode.Variable.Accept(this);
        structVariableNode.SetTypeRef(structVariableNode.Variable.TypeRef);
    }

    public void Handle(StructDeclarationNode structDeclarationNode)
    {
        SemanticContext.StartScope(ScopeType.Declaration);

        AddNodeToScope(structDeclarationNode);

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

        structDeclarationNode.SetTypeRef(
            DeclareType(
                structDeclarationNode.Name.PositionData,
                structDeclarationNode.Name.Name,
                structType
            )
        );
    }
}