using Syntax.Nodes.Statement;

namespace Syntax.NodeHandlers;

public interface IStatementNodeHandler
{
    public void Handle(AssignmentNode variableAssignmentNode);

    public void Handle(IfStatementNode ifStatementNode);

    public void Handle(ReturnNode returnNode);
}