using Syntax.Nodes.Declaration.Function;
using Syntax.Nodes.Expression;
using TypeInformation;

namespace Semantics.Passes.DeclarationPass;

public partial class DeclarationPass
{
    public void Handle(FunctionDeclarationNode functionDeclarationNode)
    {
        SemanticContext.StartScope(ScopeType.Declaration);

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

        SemanticContext.EndScope();

        functionDeclarationNode.ReturnTypeName?.Accept(this);

        var functionType = new FunctionTypeInfo(
            functionDeclarationNode.ReturnTypeName?.TypeRef,
            arguments,
            functionDeclarationNode.CanThrow
        );

        functionDeclarationNode.TypeRef = DeclareType(functionDeclarationNode.Name.Name, functionType);
    }

    public void Handle(FunctionArgumentNode functionArgumentNode)
    {
        if (functionArgumentNode.TypeName != null)
        {
            functionArgumentNode.TypeName.Accept(this);

            var typeRef = functionArgumentNode.TypeName.TypeRef;
            DeclareVariable(functionArgumentNode.Name.Name, typeRef);
            functionArgumentNode.TypeRef = typeRef;
        }
        else if (functionArgumentNode.IsDynamic)
        {
            var typeRef = TypeRef.Dynamic(SemanticContext.CurrentScope);

            functionArgumentNode.TypeRef = typeRef;

            DeclareVariable(functionArgumentNode.Name.Name, typeRef);
        }
        else
        {
            throw new Exception("Function argument must have a type");
        }
    }

    public void Handle(FunctionCallNode functionCallNode)
    {
        functionCallNode.Callee.Accept(this);

        functionCallNode.TypeRef = functionCallNode.Callee.TypeRef;

        functionCallNode.Arguments.ForEach(argument => argument.Accept(this));
    }

    public void Handle(FunctionCallArgumentNode functionCallArgumentNode)
    {
        throw new NotImplementedException();
    }
}