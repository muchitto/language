using Syntax.Nodes.Declaration.Function;
using Syntax.Nodes.Expression;
using TypeInformation;

namespace Semantics.Passes.DeclarationPass;

public partial class DeclarationPass
{
    public void Handle(FunctionDeclarationNode functionDeclarationNode)
    {
        StartScope(ScopeType.Declaration);

        AddNodeToScope(functionDeclarationNode);

        var arguments = functionDeclarationNode
            .Arguments
            .ToDictionary(
                x => x.Name.Name,
                x =>
                {
                    x.Accept(this);

                    return x.TypeRef;
                }
            );

        functionDeclarationNode.BodyContainerNode.Accept(this);

        EndScope();

        functionDeclarationNode.ReturnTypeName?.Accept(this);

        var functionType = new FunctionTypeInfo(
            functionDeclarationNode.ReturnTypeName?.TypeRef,
            arguments,
            functionDeclarationNode.CanThrow
        );

        functionDeclarationNode.PropagateTypeRef(DeclareType(
            functionDeclarationNode.Name.PositionData,
            functionDeclarationNode.Name.Name,
            functionType
        ));
    }

    public void Handle(FunctionArgumentNode functionArgumentNode)
    {
        AddNodeToScope(functionArgumentNode);

        if (functionArgumentNode.TypeName != null)
        {
            functionArgumentNode.TypeName.Accept(this);

            var typeRef = functionArgumentNode.TypeName.TypeRef;
            DeclareVariable(functionArgumentNode.PositionData, functionArgumentNode.Name.Name, typeRef);
            functionArgumentNode.PropagateTypeRef(typeRef);
        }
        else if (functionArgumentNode.IsDynamic)
        {
            var typeRef = SemanticContext.DynamicType();

            functionArgumentNode.PropagateTypeRef(typeRef);

            DeclareVariable(functionArgumentNode.PositionData, functionArgumentNode.Name.Name, typeRef);
        }
        else
        {
            throw new Exception("Function argument must have a type");
        }
    }

    public void Handle(FunctionCallNode functionCallNode)
    {
        AddNodeToScope(functionCallNode);

        functionCallNode.Callee.Accept(this);

        functionCallNode.PropagateTypeRef(functionCallNode.Callee.TypeRef);

        functionCallNode.Arguments.ForEach(argument => argument.Accept(this));
    }

    public void Handle(FunctionCallArgumentNode functionCallArgumentNode)
    {
        throw new NotImplementedException();
    }
}