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

/*
 * Just go through the top level declarations and add them to the symbol table as unknowns
 * so in the next pass, we can fetch them and add them to the symbol table as knowns
 */
public class DeclarationPass : SemanticPass, INodeHandler
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
        SemanticContext.StartScope(ScopeType.Regular);

        bodyContainerDeclarationNode.Statements.ForEach(statement => statement.Accept(this));

        SemanticContext.EndScope();
    }

    public void Handle(ProgramContainerNode programContainerNode)
    {
        SemanticContext.StartScope(ScopeType.Regular);

        CreateBaseTypes();

        programContainerNode.Statements.ForEach(statement => statement.Accept(this));

        SemanticContext.EndScope();
    }

    public void Handle(FieldAccessNode fieldAccessNode)
    {
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
        throw new NotImplementedException();
    }

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

    public void Handle(StatementListContainerNode statementListContainerNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(LiteralNode literalNode)
    {
        throw new NotImplementedException();
    }

    private void CreateBaseTypes()
    {
        SemanticContext.SetupDeclaration("Int", new TypeRef(SemanticContext.CurrentScope, new IntTypeInfo(32)));
        SemanticContext.SetupDeclaration("Float", new TypeRef(SemanticContext.CurrentScope, new FloatTypeInfo(64)));
        SemanticContext.SetupDeclaration("Bool", new TypeRef(SemanticContext.CurrentScope, new BoolTypeInfo()));
        SemanticContext.SetupDeclaration("String", new TypeRef(SemanticContext.CurrentScope, new StringTypeInfo()));
        SemanticContext.SetupDeclaration("Char", new TypeRef(SemanticContext.CurrentScope, new CharTypeInfo()));
        SemanticContext.SetupDeclaration("Void", new TypeRef(SemanticContext.CurrentScope, new VoidTypeInfo()));
        SemanticContext.SetupDeclaration("Nil", new TypeRef(SemanticContext.CurrentScope, new NilTypeInfo()));
        SemanticContext.SetupDeclaration("Dynamic", new TypeRef(SemanticContext.CurrentScope, new DynamicTypeInfo()));

        SemanticContext.SetupDeclaration("Int8", new TypeRef(SemanticContext.CurrentScope, new IntTypeInfo(8)));
        SemanticContext.SetupDeclaration("Int16", new TypeRef(SemanticContext.CurrentScope, new IntTypeInfo(16)));
        SemanticContext.SetupDeclaration("Int32", new TypeRef(SemanticContext.CurrentScope, new IntTypeInfo(32)));
        SemanticContext.SetupDeclaration("Int64", new TypeRef(SemanticContext.CurrentScope, new IntTypeInfo(64)));

        SemanticContext.SetupDeclaration("Float32", new TypeRef(SemanticContext.CurrentScope, new FloatTypeInfo(32)));
        SemanticContext.SetupDeclaration("Float64", new TypeRef(SemanticContext.CurrentScope, new FloatTypeInfo(64)));
    }

    public override void Run(ProgramContainerNode ast, SemanticContext semanticContext)
    {
        SemanticContext = semanticContext;

        ast.Accept(this);
    }
}