using Syntax.Nodes;
using TypeInformation;

namespace Semantics.Passes.DeclarationPass;

public partial class DeclarationPass
{
    public void Handle(BodyContainerNode bodyContainerDeclarationNode)
    {
        StartScope(ScopeType.Regular);

        AddNodeToScope(bodyContainerDeclarationNode);

        bodyContainerDeclarationNode.Statements.ForEach(statement => statement.Accept(this));

        EndScope();
    }

    public void Handle(ProgramContainerNode programContainerNode)
    {
        StartScope(ScopeType.Regular);

        AddNodeToScope(programContainerNode);

        CreateBaseTypes(programContainerNode);

        programContainerNode.Statements.ForEach(statement => statement.Accept(this));

        EndScope();
    }
}