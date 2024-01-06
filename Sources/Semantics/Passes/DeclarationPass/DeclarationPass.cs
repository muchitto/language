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
using TypeInformation;

namespace Semantics.Passes.DeclarationPass;

/*
 * Just go through the top level declarations and add them to the symbol table as unknowns
 * so in the next pass, we can fetch them and add them to the symbol table as knowns
 */
public partial class DeclarationPass : SemanticPass, INodeHandler
{
    public void Handle(ReturnNode returnNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(IdentifierNode identifierNode)
    {
        identifierNode.TypeRef = ReferenceVariable(identifierNode.Name);
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
        if (variableDeclarationNode.Type != null)
        {
            variableDeclarationNode.Type.Accept(this);

            variableDeclarationNode.TypeRef = variableDeclarationNode.Type.TypeRef;
        }
        else if (variableDeclarationNode.IsDynamic)
        {
            variableDeclarationNode.TypeRef = TypeRef.Dynamic(SemanticContext.CurrentScope);
        }
        else if (variableDeclarationNode.Value != null)
        {
            variableDeclarationNode.Value.Accept(this);

            variableDeclarationNode.TypeRef = variableDeclarationNode.Value.TypeRef;
        }
        else
        {
            throw new Exception("Variable must have a type");
        }
    }

    public void Handle(AssignmentNode variableAssignmentNode)
    {
        variableAssignmentNode.Name.Accept(this);
        variableAssignmentNode.Value.Accept(this);
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
        fieldAccessNode.Left.Accept(this);
        fieldAccessNode.Right.Accept(this);
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
        numberLiteralNode.TypeRef = numberLiteralNode.Value.Contains('.')
            ? TypeRef.Float(SemanticContext.CurrentScope)
            : TypeRef.Int(SemanticContext.CurrentScope);
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
        identifierTypeNode.TypeRef = ReferenceType(identifierTypeNode.Name);
    }

    public void Handle(FunctionTypeArgumentNode functionTypeArgumentNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(FunctionDeclarationNode functionDeclarationNode)
    {
        functionDeclarationNode.Arguments.ForEach(argument => { argument.TypeName?.Accept(this); });

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

            functionArgumentNode.TypeRef = functionArgumentNode.TypeName.TypeRef;
        }
        else if (functionArgumentNode.IsDynamic)
        {
            functionArgumentNode.TypeRef = TypeRef.Dynamic(SemanticContext.CurrentScope);
        }
        else
        {
            throw new Exception("Function argument must have a type");
        }
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
        SemanticContext.StartScope(ScopeType.Declaration);

        var fields = structDeclarationNode
            .Fields
            .ToDictionary(
                x => x.Name,
                x =>
                {
                    x.Accept(this);

                    return x.TypeRef;
                }
            );

        var structType = new StructTypeInfo(fields);

        SemanticContext.EndScope();

        structDeclarationNode.TypeRef = DeclareType(structDeclarationNode.Name.Name, structType);
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
        DeclareType("Int", new IntTypeInfo(32));
        DeclareType("Float", new FloatTypeInfo(64));
        DeclareType("Bool", new BoolTypeInfo());
        DeclareType("String", new StringTypeInfo());
        DeclareType("Char", new CharTypeInfo());
        DeclareType("Void", new VoidTypeInfo());
        DeclareType("Nil", new NilTypeInfo());
        DeclareType("Dynamic", new DynamicTypeInfo());

        DeclareType("Int8", new IntTypeInfo(8));
        DeclareType("Int16", new IntTypeInfo(16));
        DeclareType("Int32", new IntTypeInfo(32));
        DeclareType("Int64", new IntTypeInfo(64));

        DeclareType("Float32", new FloatTypeInfo(32));
        DeclareType("Float64", new FloatTypeInfo(64));
    }

    public override void Run(ProgramContainerNode ast, SemanticContext semanticContext)
    {
        SemanticContext = semanticContext;

        ast.Accept(this);
    }
}