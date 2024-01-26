using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations.Struct;
using Syntax.Nodes;
using Syntax.Nodes.Declaration;
using Syntax.Nodes.Declaration.Closure;
using Syntax.Nodes.Declaration.Enum;
using Syntax.Nodes.Declaration.Function;
using Syntax.Nodes.Declaration.Struct;
using IdentifierNode = Syntax.Nodes.IdentifierNode;

namespace Semantics.Passes.DeclarationPass;

public class DeclarationPass(SemanticContext semanticContext)
    : SemanticPass(semanticContext), ICommonPass
{
    public void Handle(IdentifierNode identifierNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(FieldAccessNode fieldAccessNode)
    {
    }

    public void Handle(ArrayAccessNode arrayAccessNode)
    {
    }

    public void Handle(BodyContainerNode bodyContainerDeclarationNode)
    {
        StartCodeBlockScope(bodyContainerDeclarationNode);

        HandleBlock(bodyContainerDeclarationNode);

        EndScope();
    }

    public void Handle(ProgramContainerNode programContainerNode)
    {
        StartCodeBlockScope(programContainerNode);

        HandleBlock(programContainerNode);

        EndScope();
    }

    public void Handle(CodeBlockNode codeBlockNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(DeclarationNameNode declarationNameNode)
    {
        SemanticContext.Declaration(declarationNameNode);
    }

    public void Handle(StructFunctionNode structFunctionNode)
    {
        structFunctionNode.Function.Accept(this);
    }

    public void Handle(StructVariableNode structVariableNode)
    {
        structVariableNode.Variable.Accept(this);
    }

    public void Handle(FunctionDeclarationNode functionDeclarationNode)
    {
        SemanticContext.Declaration(functionDeclarationNode.Name);

        StartDeclarationScope(functionDeclarationNode.Name);

        foreach (var argument in functionDeclarationNode.Arguments)
        {
            SemanticContext.Declaration(argument.Name);
        }

        functionDeclarationNode.Body.Accept(this);

        EndScope();
    }

    public void Handle(StructDeclarationNode structDeclarationNode)
    {
        SemanticContext.Declaration(structDeclarationNode.Name);

        foreach (var field in structDeclarationNode.Fields)
        {
            if (field is not INodeAcceptor<IStructChildNodeHandler> fieldNode)
            {
                continue;
            }

            fieldNode.Accept(this);
        }
    }

    public void Handle(EnumDeclarationNode enumDeclarationNodeDeclaration)
    {
        throw new NotImplementedException();
    }

    public void Handle(ClosureDeclarationNode closureDeclarationNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(VariableDeclarationNode variableDeclarationNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(AnnotationNode annotationNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(AnnotationsNode annotationsNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(AnnotationArgumentNode annotationArgumentNode)
    {
        throw new NotImplementedException();
    }

    private void HandleBlock(CodeBlockNode codeBlockNode)
    {
        foreach (var node in codeBlockNode.Statements)
        {
            switch (node)
            {
                case StructDeclarationNode statementListNode:
                    statementListNode.Accept(this);
                    break;
                case FunctionDeclarationNode statementListNode:
                    statementListNode.Accept(this);
                    break;
                case INodeAcceptor<IBasicNodeHandler> statementListNode:
                    statementListNode.Accept(this);
                    break;
            }
        }
    }

    public override void Run(ProgramContainerNode ast)
    {
        ast.Accept(this);
    }
}