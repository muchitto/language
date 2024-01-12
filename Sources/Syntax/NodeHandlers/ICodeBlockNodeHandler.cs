using Syntax.Nodes;

namespace Syntax.NodeHandlers;

public interface IStatementListNodeHandler
{
    public void Handle(BodyContainerNode bodyContainerDeclarationNode);

    public void Handle(ProgramContainerNode programContainerNode);
}