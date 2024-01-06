using Parsing.NodeHandlers;
using Parsing.Nodes;
using Parsing.Nodes.Declaration;
using Parsing.Nodes.Declaration.Enum;
using Parsing.Nodes.Declaration.Function;
using Parsing.Nodes.Declaration.Interface;
using Parsing.Nodes.Declaration.Struct;
using Parsing.Nodes.Type;
using Parsing.Nodes.Type.Function;
using Parsing.Nodes.Type.Struct;
using Parsing.Nodes.Type.Tuple;
using TypeInformation;

namespace Semantics.Passes;

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
        throw new NotImplementedException();
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
        bodyContainerDeclarationNode.Statements.ForEach(statement => statement.Accept(this));
    }

    public void Handle(ProgramContainerNode programContainerNode)
    {
        programContainerNode.Statements.ForEach(statement => statement.Accept(this));
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

    public void Handle(InterfaceDeclarationNode interfaceDeclarationNodeDeclaration)
    {
        throw new NotImplementedException();
    }

    public void Handle(StructDeclarationNode structDeclarationNode)
    {
        var result = SemanticContext.LookupTypeRef(structDeclarationNode.Name.Name);

        if (result.ResultType == SymbolResultType.NotDeclared)
        {
            throw new Exception($"Struct {structDeclarationNode.Name.Name} not found");
        }
    }

    public void Handle(StructFunctionNode structFunctionNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(StructVariableNode structVariableNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(StatementListContainerNode statementListContainerNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(LiteralNode literalNode)
    {
        throw new NotImplementedException();
    }

    public override void Run(ProgramContainerNode ast, SemanticContext semanticContext)
    {
        SemanticContext = semanticContext;
        ast.Accept(this);
    }
}