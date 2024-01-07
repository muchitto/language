using Syntax.NodeHandlers;
using Syntax.Nodes;
using Syntax.Nodes.Declaration;
using Syntax.Nodes.Declaration.Enum;
using Syntax.Nodes.Declaration.Function;
using Syntax.Nodes.Declaration.Interface;
using Syntax.Nodes.Declaration.Struct;
using Syntax.Nodes.Expression;
using Syntax.Nodes.Literal;
using Syntax.Nodes.Statement;
using Syntax.Nodes.Type;
using Syntax.Nodes.Type.Function;
using Syntax.Nodes.Type.Struct;
using Syntax.Nodes.Type.Tuple;

namespace Semantics.Passes.TypeResolution;

public class TypeResolution : SemanticPass, INodeHandler
{
    public void Handle(ReturnNode returnNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(IdentifierNode identifierNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(ExpressionNode expressionNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(AnnotationNode annotationNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(AnnotationArgumentListNode annotationArgumentListNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(AnnotationArgumentNode annotationArgumentNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(TypeAliasDeclarationNode typeAliasDeclarationNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(VariableDeclarationNode variableDeclarationNode)
    {
        variableDeclarationNode.Type.Accept(this);
    }

    public void Handle(AssignmentNode variableAssignmentNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(IfStatementNode ifStatementNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(BodyContainerNode bodyContainerDeclarationNode)
    {
        StartScopeFromNode(bodyContainerDeclarationNode);

        bodyContainerDeclarationNode.Statements.ForEach(statement => statement.Accept(this));

        EndScope();
    }

    public void Handle(ProgramContainerNode programContainerNode)
    {
        StartScopeFromNode(programContainerNode);

        programContainerNode.Statements.ForEach(statement => statement.Accept(this));

        EndScope();
    }

    public void Handle(FieldAccessNode fieldAccessNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(ArrayAccessNode arrayAccessNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(StructLiteralNode structLiteralNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(StructLiteralFieldNode structLiteralFieldNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(TupleLiteralNode tupleLiteralNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(TupleLiteralFieldNode tupleLiteralFieldNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(StringLiteralNode stringLiteralNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(NilLiteralNode nullLiteralNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(BooleanLiteralNode booleanLiteralNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(NumberLiteralNode numberLiteralNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(CharLiteralNode charLiteralNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(TypeNode typeNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(StructTypeNode structTypeNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(StructTypeFieldNode structTypeFieldNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(FunctionTypeNode functionTypeNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(TupleTypeNode tupleTypeNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(TupleTypeFieldNode tupleTypeFieldNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(IdentifierTypeNode identifierTypeNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(FunctionTypeArgumentNode functionTypeArgumentNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(StructDeclarationNode structDeclarationNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(StructFunctionNode structFunctionNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(StructVariableNode structVariableNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(InterfaceDeclarationNode interfaceDeclarationNodeDeclaration)
    {
        throw new NotImplementedException();
    }

    public void Handle(EnumDeclarationNode enumDeclarationNodeDeclaration)
    {
        throw new NotImplementedException();
    }

    public void Handle(EnumFunctionNode enumFunctionNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(EnumCaseAssociatedValueNode enumCaseAssociatedValueNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(EnumCaseNode enumCaseNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(FunctionDeclarationNode functionDeclarationNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(FunctionArgumentNode functionArgumentNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(FunctionCallNode functionCallNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(FunctionCallArgumentNode functionCallArgumentNode)
    {
        throw new NotImplementedException();
    }

    public override void Run(ProgramContainerNode ast, SemanticContext semanticContext)
    {
        SemanticContext = semanticContext;

        ast.Accept(this);
    }
}