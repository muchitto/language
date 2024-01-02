using Parsing.NodeHandlers;
using Parsing.Nodes;
using TypeInformation;

namespace Semantics.Passes;

/*
 * Just go through the top level declarations and add them to the symbol table as unknowns
 * so in the next pass, we can fetch them and add them to the symbol table as knowns
 */
public class TypeRefAssignment : SemanticPass, INodeHandler
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

    public void Handle(StatementListContainerNode statementListContainerNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(BodyContainerNode bodyContainerDeclarationNode)
    {
        SemanticContext.StartScope();

        bodyContainerDeclarationNode.BodyAccept(this);

        SemanticContext.EndScope();
    }

    public void Handle(ProgramContainerNode programContainerNode)
    {
        SemanticContext.StartScope();

        CreateBaseTypes();

        programContainerNode.BodyAccept(this);

        SemanticContext.EndScope();
    }

    public void Handle(FieldAccessNode fieldAccessNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(ArrayAccessNode arrayAccessNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(LiteralNode literalNode)
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
        SemanticContext.StartScope();

        functionDeclarationNode.BodyAccept(this);

        SemanticContext.EndScope();
    }

    public void Handle(FunctionArgumentListNode functionArgumentListNode)
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
        SemanticContext.StartScope();

        enumDeclarationNodeDeclaration.BodyAccept(this);

        SemanticContext.EndScope();
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
        SemanticContext.StartScope();

        structDeclarationNode.BodyAccept(this);

        SemanticContext.EndScope();
    }

    public void Handle(StructFieldNode structFieldNode)
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

    private void CreateBaseTypes()
    {
        SemanticContext.Add("Int", new IntTypeInfo(32));
        SemanticContext.Add("Int8", new IntTypeInfo(8));
        SemanticContext.Add("Int16", new IntTypeInfo(16));
        SemanticContext.Add("Int32", new IntTypeInfo(32));
        SemanticContext.Add("Float", new FloatTypeInfo(64));
        SemanticContext.Add("Float32", new FloatTypeInfo(32));
        SemanticContext.Add("Float64", new FloatTypeInfo(64));
        SemanticContext.Add("Bool", new BoolTypeInfo());
        SemanticContext.Add("String", new StringTypeInfo());
        SemanticContext.Add("Char", new CharTypeInfo());
        SemanticContext.Add("Void", new VoidTypeInfo());
        SemanticContext.Add("Nil", new NilTypeInfo());
        SemanticContext.Add("Dynamic", new DynamicTypeInfo());
    }

    public override void Run(ProgramContainerNode ast, SemanticContext semanticContext)
    {
        SemanticContext = semanticContext;

        ast.Accept(this);
    }
}