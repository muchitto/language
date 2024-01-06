using Syntax.NodeHandlers;
using Syntax.Nodes;
using Syntax.Nodes.Declaration;
using Syntax.Nodes.Declaration.Enum;
using Syntax.Nodes.Declaration.Interface;
using Syntax.Nodes.Statement;
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

    public void Handle(FieldAccessNode fieldAccessNode)
    {
        fieldAccessNode.Left.Accept(this);
        fieldAccessNode.Right.Accept(this);
    }

    public void Handle(ArrayAccessNode arrayAccessNode)
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