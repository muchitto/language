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

    public void HandleStart(StatementListContainerNode statementListContainerNode);

    public void HandleEnd(StatementListContainerNode statementListContainerNode);

    public void HandleStart(BodyContainerNode bodyContainerDeclarationNode);
    public void HandleEnd(BodyContainerNode bodyContainerDeclarationNode);

    public void HandleStart(ProgramContainerNode programContainerNode);
    public void HandleEnd(ProgramContainerNode programContainerNode);
}