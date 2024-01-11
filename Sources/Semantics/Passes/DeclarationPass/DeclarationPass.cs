using Syntax.NodeHandlers;
using Syntax.Nodes;
using Syntax.Nodes.Declaration;
using Syntax.Nodes.Declaration.Closure;
using Syntax.Nodes.Declaration.Enum;
using Syntax.Nodes.Declaration.Interface;
using Syntax.Nodes.Expression;
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
        AddNodeToScope(identifierNode);

        identifierNode.PropagateTypeRef(ReferenceVariable(identifierNode.Name));
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

    public void Handle(TypeAliasDeclarationNode typeAliasDeclarationNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(VariableDeclarationNode variableDeclarationNode)
    {
        AddNodeToScope(variableDeclarationNode);

        TypeRef? typeRef = null;
        if (variableDeclarationNode.Type != null)
        {
            variableDeclarationNode.Type.Accept(this);

            typeRef = variableDeclarationNode.Type.TypeRef;
        }
        else if (variableDeclarationNode.IsDynamic)
        {
            typeRef = SemanticContext.DynamicType();
        }
        else if (variableDeclarationNode.Value != null)
        {
            variableDeclarationNode.Value.Accept(this);

            typeRef = variableDeclarationNode.Value.TypeRef;
        }
        else
        {
            typeRef = TypeRef.Unknown(SemanticContext.CurrentScope);
        }

        variableDeclarationNode.PropagateTypeRef(typeRef);

        DeclareVariable(
            variableDeclarationNode.PositionData,
            variableDeclarationNode.Name.Name,
            typeRef
        );
    }

    public void Handle(AssignmentNode variableAssignmentNode)
    {
        AddNodeToScope(variableAssignmentNode);

        variableAssignmentNode.Name.Accept(this);
        variableAssignmentNode.Value.Accept(this);

        variableAssignmentNode.PropagateTypeRef(variableAssignmentNode.Name.TypeRef);

        if (variableAssignmentNode.Name.TypeRef.IsUnknown && !variableAssignmentNode.Value.TypeRef.IsUnknown)
        {
            variableAssignmentNode.TypeRef.TypeInfo = variableAssignmentNode.Value.TypeRef.TypeInfo;
        }
        else if (!variableAssignmentNode.Name.TypeRef.IsUnknown && !variableAssignmentNode.Value.TypeRef.IsUnknown)
        {
            if (variableAssignmentNode.Name.TypeRef.TypeId != variableAssignmentNode.Value.TypeRef.TypeId)
            {
                throw new Exception("Types are not equal");
            }
        }
    }

    public void Handle(IfStatementNode ifStatementNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(FieldAccessNode fieldAccessNode)
    {
        AddNodeToScope(fieldAccessNode);

        fieldAccessNode.Left.Accept(this);
        fieldAccessNode.Right.Accept(this);

        fieldAccessNode.PropagateTypeRef(fieldAccessNode.Right.TypeRef);
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

    public void Handle(IfExpressionNode ifExpressionNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(BinaryOpNode binaryOpNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(BodyExpressionNode bodyExpressionNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(ClosureNode closureNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(ClosureArgumentNode closureArgumentNode)
    {
        throw new NotImplementedException();
    }

    private void CreateBaseTypes(BaseNode ast)
    {
        DeclareType(ast.PositionData, "Int", new IntTypeInfo(32));
        DeclareType(ast.PositionData, "Float", new FloatTypeInfo(64));
        DeclareType(ast.PositionData, "Bool", new BoolTypeInfo());
        DeclareType(ast.PositionData, "String", new StringTypeInfo());
        DeclareType(ast.PositionData, "Char", new CharTypeInfo());
        DeclareType(ast.PositionData, "Void", new VoidTypeInfo());
        DeclareType(ast.PositionData, "Nil", new NilTypeInfo());
        DeclareType(ast.PositionData, "Dynamic", new DynamicTypeInfo());

        DeclareType(ast.PositionData, "Int8", new IntTypeInfo(8));
        DeclareType(ast.PositionData, "Int16", new IntTypeInfo(16));
        DeclareType(ast.PositionData, "Int32", new IntTypeInfo(32));
        DeclareType(ast.PositionData, "Int64", new IntTypeInfo(64));

        DeclareType(ast.PositionData, "Float32", new FloatTypeInfo(32));
        DeclareType(ast.PositionData, "Float64", new FloatTypeInfo(64));
    }

    public override void Run(ProgramContainerNode ast, SemanticContext semanticContext)
    {
        SemanticContext = semanticContext;

        ast.Accept(this);
    }
}