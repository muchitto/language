using Parsing.Node;
using Parsing.Node.Declaration;
using Parsing.Node.Statement;
using Parsing.Nodes;

namespace Parsing.NodeHandlers;

public interface IBasicNodeHandler
{
    public void Handle(ReturnNode returnNode);

    public void Handle(IdentifierNode identifierNode);

    public void Handle(ExpressionNode expressionNode);

    public void Handle(AnnotationNode annotationNode);

    public void Handle(AnnotationArgumentListNode annotationArgumentListNode);

    public void Handle(AnnotationArgumentNode annotationArgumentNode);

    public void Handle(TypeAliasDeclarationNode typeAliasDeclarationNode);

    public void Handle(VariableDeclarationNode variableDeclarationNode);

    public void Handle(AssignmentNode variableAssignmentNode);

    public void Handle(IfStatementNode ifStatementNode);

    public void Handle(BodyContainerNode bodyContainerDeclarationNode);

    public void Handle(ProgramContainerNode programContainerNode);

    public void Handle(FieldAccessNode fieldAccessNode);

    public void Handle(ArrayAccessNode arrayAccessNode);
}