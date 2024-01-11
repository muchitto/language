using Syntax.Nodes.Declaration.Struct;
using TypeInformation;

namespace Semantics.Passes.DeclarationPass;

public partial class DeclarationPass
{
    public void Handle(StructFunctionNode structFunctionNode)
    {
        AddNodeToScope(structFunctionNode);

        structFunctionNode.Function.Accept(this);
        structFunctionNode.PropagateTypeRef(structFunctionNode.Function.TypeRef);
    }

    public void Handle(StructVariableNode structVariableNode)
    {
        AddNodeToScope(structVariableNode);

        structVariableNode.Variable.Accept(this);
        structVariableNode.PropagateTypeRef(structVariableNode.Variable.TypeRef);
    }

    public void Handle(StructDeclarationNode structDeclarationNode)
    {
        StartScope(ScopeType.Declaration);

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

        EndScope();

        structDeclarationNode.PropagateTypeRef(
            DeclareType(
                structDeclarationNode.Name.PositionData,
                structDeclarationNode.Name.Name,
                structType
            )
        );
    }
}